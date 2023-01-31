using Basket.Entities;
using Basket.Repository.Interfaces;

namespace Basket.Repository
{
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
}
