using AspnetRunBasics.Models;

namespace AspnetRunBasics.Services;

public interface IBasketService
{
    Task<BasketModel> GetBasket(string username);
    Task<BasketModel> UpdateBasket(BasketModel basket);
    Task CheckoutBasket(BasketCheckoutModel basketCheckout);
}
