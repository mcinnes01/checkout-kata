using Checkout.Contracts;
using System.Collections.Generic;

namespace Checkout.Models
{
    public class Bag : IBag
    {
        public IList<BaggedItem> BaggedItems { get; } = new List<BaggedItem>();
        public void Add(Product product, int quantity)
        {
            BaggedItems.Add(new BaggedItem { Product = product, Quantity = quantity });
        }
    }

    public class BaggedItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
