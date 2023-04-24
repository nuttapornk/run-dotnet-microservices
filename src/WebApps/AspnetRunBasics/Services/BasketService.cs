using AspnetRunBasics.Models;
using AspnetRunBasics.Extensions;

namespace AspnetRunBasics.Services;

public class BasketService : IBasketService
{
    private readonly HttpClient _client;
    public BasketService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<BasketModel> GetBasket(string username)
    {
        var response = await _client.GetAsync($"/basket/{username}");
        return await response.ReadContentAsync<BasketModel>();        
    }

    public async Task<BasketModel> UpdateBasket(BasketModel basket)
    {
        var response = await _client.PostAsJson($"/basket", basket);
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException("Something went wrong when calling api.");            
        }
        return await response.ReadContentAsync<BasketModel>();
    }

    public async Task CheckoutBasket(BasketCheckoutModel basketCheckout)
    {
        var response = await _client.PostAsJson($"/basket/checkout", basketCheckout);
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException("Something went wrong when calling api.");
        }
    }
    
}
