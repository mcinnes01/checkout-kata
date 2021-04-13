namespace Checkout.Contracts
{
    public interface IDiscount
    {
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public decimal OfferPrice { get; set; }
    }
}