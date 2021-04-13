using Checkout.Contracts;

namespace Checkout.Models
{
    public class ReceiptItem : IReceiptItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public Product GetProduct() => Product;

        public int GetQuantity() => Quantity;

        public decimal GetTotal()
        {
            return Product.UnitPrice * Quantity;
        }
    }
}