using Checkout.Contracts;

namespace Checkout.Models
{
    public class ReceiptItem : IReceiptItem
    {
        private Product _product { get; }
        private int _quantity { get; }

        public ReceiptItem(Product product, int quantity)
        {
            _product = product;
            _quantity = quantity;
        }

        public Product GetProduct() => _product;

        public int GetQuantity() => _quantity;

        public decimal GetTotal()
        {
            return _product.UnitPrice * _quantity;
        }
    }
}