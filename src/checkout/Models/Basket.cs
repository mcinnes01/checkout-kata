using Checkout.Contracts;
using System.Collections.Generic;

namespace Checkout.Models
{
    public class Basket : IBasket
    {
        public IList<BasketItem> Items { get; } = new List<BasketItem>();
        public void Add(Product product, int quantity)
        {
            Items.Add(new BasketItem { Product = product, Quantity = quantity });
        }
    }

    public class BasketItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
