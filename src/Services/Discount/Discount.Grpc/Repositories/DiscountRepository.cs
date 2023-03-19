using Dapper;
using Discount.Grpc.Entities;
using Npgsql;

namespace Discount.Grpc.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DiscountRepository(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _connectionString = configuration.GetConnectionString("Discount") ?? throw new ArgumentNullException(nameof(configuration));
    }
    public async Task<Coupon> GetDiscount(string productName)
    {
        using NpgsqlConnection connection = new(_connectionString);
        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
            ("SELECT * FROM Coupon WHERE ProductName = @ProductName",new {ProductName = productName});

        if (coupon == null)
        {
            return new Coupon
            {
                ProductName = "No Discount",
                Amount = 0,
                Description = string.Empty,
            };
        }
        return coupon;
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        using NpgsqlConnection connection = new(_connectionString);
        var affected = await connection.ExecuteAsync
            ("INSERT INTO Coupon (ProductName,Description,Amount) VALUES (@ProductName,@Description,@Amount)",
            new
            {
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount,
            });

        return affected > 0;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        using NpgsqlConnection connection = new(_connectionString);
        var affected = await connection.ExecuteAsync
            ("UPDATE Coupon set ProductName = @ProductName,Description = @Description,Amount=@Amount WHERE Id=@Id",
            new
            {
                Id = coupon.Id,
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount,
            });

        return affected > 0;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        using NpgsqlConnection connection = new(_connectionString);
        var affected = await connection.ExecuteAsync
            ("DELETE FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

        return affected > 0;

    }
}
