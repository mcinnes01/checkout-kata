using Checkout.Contracts;
using Checkout.Models;
using System.Collections.Generic;
using System.Linq;

namespace Checkout
{
    public class CheckoutService : ICheckout
    {
        private readonly IEnumerable<Product> _products;
        private readonly IEnumerable<IDiscount> _discounts;
        private readonly IBag _bag;

        public CheckoutService(IEnumerable<Product> products, IEnumerable<IDiscount> discounts)
        {
            _products = products;
            _discounts = discounts;
            _bag = new Bag();
        }

        public void Scan(string sku, int quantity)
        {
            // Check the product exists
            var product = _products
                .Where(s => s.Sku == sku)
                .First();

            // Add it to the bag
            _bag.Add(product, quantity);
        }

        public decimal GetTotal()
        {
            var groups = _bag.BaggedItems
                .GroupBy(i => i.Product)
                .Select(grp => new
                {
                    Product = grp.Key,
                    Quantity = grp.Sum(g => g.Quantity)
                });

            decimal total = 0;
            foreach (var group in groups)
            {
                total += ProductTotal(group.Product, group.Quantity);
            }

            return total;
        }

        private decimal ProductTotal(Product product, int quantity)
        {
            var discount = _discounts
                .Where(d => d.Sku == product.Sku);

            if (!discount.Any())
            {
                return product.UnitPrice * quantity;
            }

            var discountRule = discount.First();
            var remainder = quantity % discountRule.Quantity;
            var discountTotal = (quantity - remainder) / discountRule.Quantity * discountRule.OfferPrice;
            var remainderTotal = remainder * product.UnitPrice;

            return discountTotal + remainderTotal;
        }
    }
}
