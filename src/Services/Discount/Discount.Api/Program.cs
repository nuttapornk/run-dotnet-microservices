using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Npgsql;
using Polly;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(a =>
{
    a.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Discount.Api",
        Version = "v1"
    });
});

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Discount"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(a =>
    {
        a.SwaggerEndpoint("/swagger/v1/swagger.json", "Discount.Api v1");
    });
}

using (var scope = app.Services.CreateScope())
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


static void ExecuteMigrations(IConfiguration configuration)
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

app.Run();
