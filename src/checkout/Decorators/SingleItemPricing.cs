using Checkout.Contracts;
using System.Collections.Generic;

namespace Checkout.Decorators
{
    public class SingleItemPricing : PricingDecorator
    {
        public SingleItemPricing(IEnumerable<IDiscount> discounts, IReceiptItem item)
            : base(discounts, item)
        {
        }
    }
}