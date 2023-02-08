using Basket.Api.Entities;

namespace Basket.Api.Repository.Interfaces;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string username);
    Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
    Task DeleteBasket(string username);
}
