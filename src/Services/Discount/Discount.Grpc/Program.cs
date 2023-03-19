using Discount.Grpc.Repositories;
using Discount.Grpc.Services;
using Npgsql;
using Polly;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddScoped<IDiscountRepository,DiscountRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<DiscountService>();

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        var retry = Policy.Handle<NpgsqlException>()
            .WaitAndRetry(retryCount: 5,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // 2,4,8,16,32 sc
                onRetry: (exception, retryCount, context) =>
                {
                    logger.LogError($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
                });

        retry.Execute(() => ExecuteMigrations(builder.Configuration));
        logger.LogInformation("Migrated postresql database.");

    }
    catch (Exception ex)
    {

        logger.LogError(ex, "An error occurred sedding the database");
    }
}

static void ExecuteMigrations(ConfigurationManager configuration)
{
    using NpgsqlConnection connection = new(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
    connection.Open();

    using var cmd = new NpgsqlCommand
    {
        Connection = connection
    };

    cmd.CommandText = "DROP TABLE IF EXISTS Coupon";
    cmd.ExecuteNonQuery();

    cmd.CommandText = @"CREATE TABLE Coupon(
                        Id SERIAL PRIMARY KEY, 
                        ProductName VARCHAR(24) NOT NULL,
                        Description TEXT,
                        Amount INT)";
    cmd.ExecuteNonQuery();

    cmd.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
    cmd.ExecuteNonQuery();

    cmd.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
    cmd.ExecuteNonQuery();
}

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
