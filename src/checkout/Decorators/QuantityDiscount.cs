using Checkout.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Checkout.Decorators
{
    public class QuantityDiscount : PricingDecorator
    {
        public QuantityDiscount(IEnumerable<IDiscount> discounts, IReceiptItem item)
            : base(discounts, item)
        {
        }

        public override decimal GetTotal()
        {
            var discount = Discounts
                .Where(d => d.Sku == GetProduct().Sku);

            // If there are no matching discount return
            if (!discount.Any())
            {
                return base.GetTotal();
            }

            var discountRule = discount.First();
            var remainder = GetQuantity() % discountRule.Quantity;
            var discountQuantity = GetQuantity() - remainder;
            var discountPrice = discountQuantity / discountRule.Quantity * discountRule.OfferPrice;
            var standardPrice = discountQuantity * GetProduct().UnitPrice;
            var discountTotal = standardPrice - discountPrice;

            return base.GetTotal() - discountTotal;
        }
    }
}