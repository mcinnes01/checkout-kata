using Checkout.Contracts;
using Checkout.Decorators;
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

        public CheckoutService(IEnumerable<Product> products,
            IEnumerable<IDiscount> discounts)
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
            // These are the items scanned in to the till
            var receiptItems = _bag.BaggedItems
                .GroupBy(i => i.Product)
                .Select(grp => new ReceiptItem
                {
                    Product = grp.Key,
                    Quantity = grp.Sum(g => g.Quantity)
                });

            decimal subtotal = 0;
            // Loop over the receipt items and apply the discounts
            foreach (var item in receiptItems)
            {
                // Apply the single item pricing first to get our base price without discounts
                IReceiptItem singleItem = new SingleItemPricing(_discounts, item);
                // Run the item through any subsequent discounts
                IReceiptItem quantityDiscount = new QuantityDiscount(_discounts, singleItem);
                // Add the item price to the subtotal
                subtotal += quantityDiscount.GetTotal();
            }

            return subtotal;
        }
    }
}