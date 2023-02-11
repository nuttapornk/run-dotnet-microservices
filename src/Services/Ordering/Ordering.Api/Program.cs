using Microsoft.EntityFrameworkCore;
using Ordering.Api;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddWebApiServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
	try
	{
		var context = service.GetRequiredService<ApplicationDbContext>();
		context.Database.Migrate();
	}
	catch (Exception ex)
	{
		var logger = service.GetRequiredService<ILogger<Program>>();
		logger.LogError(ex, "An error occurred sedding the database.");
	}
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
