namespace AspnetRunBasics.Models;

public class OrderResponseModel
{
    public string? Username { get; set; }
    public decimal TotalPrice { get; set; }

    // BillingAddress
    public string? FirstName { get; set; }
    public string? LastName { get; set; } 
    public string? Email { get; set; } 
    public string? Address { get; set; } 
    public string? Country { get; set; } 
    public string? State { get; set; }
    public string? ZipCode { get; set; } 

    // Payment
    public string? CardName { get; set; }
    public string? CardNumber { get; set; }
    public string? Expiration { get; set; }
    public string? Cvv { get; set; }
    public int PaymentMethod { get; set; }
}
