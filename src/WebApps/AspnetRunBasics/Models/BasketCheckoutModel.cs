namespace AspnetRunBasics.Models;

public class BasketCheckoutModel
{
    public string Username { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string? CardName { get; set; }
    public string? CardNumber { get; set; }
    public string? Expiration { get; set; } 
    public string? Cvv { get; set; }
    public int PaymentMethod { get; set; }
}
