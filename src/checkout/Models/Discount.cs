using Checkout.Contracts;

namespace Checkout.Models
{
    public class Discount : IDiscount
    {
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public decimal OfferPrice { get; set; }
    }
}