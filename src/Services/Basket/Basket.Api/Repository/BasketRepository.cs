using Basket.Api.Entities;
using Basket.Api.Repository.Interfaces;

namespace Basket.Api.Repository;

public class BasketRepository : IBasketRepository
{
    public Task DeleteBasket(string username)
    {
        throw new NotImplementedException();
    }

    public Task<ShoppingCart> GetBasket(string username)
    {
        throw new NotImplementedException();
    }

    public Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
    {
        throw new NotImplementedException();
    }
}
