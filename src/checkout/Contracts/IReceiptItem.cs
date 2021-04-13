using Checkout.Models;

namespace Checkout.Contracts
{
    public interface IReceiptItem
    {
        decimal GetTotal();

        Product GetProduct();

        int GetQuantity();
    }
}