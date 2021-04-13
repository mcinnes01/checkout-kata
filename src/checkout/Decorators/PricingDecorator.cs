using Checkout.Contracts;
using Checkout.Models;
using System.Collections.Generic;

namespace Checkout.Decorators
{
    public abstract class PricingDecorator : IReceiptItem
    {
        protected IEnumerable<IDiscount> Discounts;
        private readonly IReceiptItem Item;

        public PricingDecorator(IEnumerable<IDiscount> discounts, IReceiptItem item)
        {
            Discounts = discounts;
            Item = item;
        }

        public Product GetProduct() => Item.GetProduct();

        public int GetQuantity() => Item.GetQuantity();

        public virtual decimal GetTotal() => Item.GetTotal();
    }
}