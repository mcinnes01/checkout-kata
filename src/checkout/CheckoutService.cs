using Checkout.Contracts;
using Checkout.Models;
using System.Collections.Generic;
using System.Linq;

namespace Checkout
{
    public class CheckoutService : ICheckout
    {
        private readonly IEnumerable<Product> _products;
        private readonly IBasket _basket;

        public CheckoutService(IEnumerable<Product> products)
        {
            _products = products;
            _basket = new Basket();
        }

        public void Scan(string sku, int quantity)
        {
            // Check the product exists
            var product = _products
                .Where(s => s.Sku == sku)
                .First();

            // Add it to the basket
            _basket.Add(product, quantity);
        }

        public decimal GetTotal()
        {
            var group = _basket.Items
                .GroupBy(i => i.Product)
                .Select(grp => new
                {
                    Product = grp.Key,
                    Quantity = grp.Sum(g => g.Quantity)
                });
            var total = group.Sum(t => t.Product.UnitPrice * t.Quantity);
            return total;
        }
    }
}
