namespace Basket.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {

        }
        public ShoppingCart(string username)
        {
            Username = username;
        }

        public decimal TotalPrice
        {
            get
            {
                if (Items.Count == 0) return 0;
                return Items.Sum(x => x.Price * x.Quantity);
            }
        }

        public string Username { get; set; } = string.Empty;
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    }
}
