using Checkout.Models;

namespace Checkout.Contracts
{
    public interface IBagPolicy
    {
        public Country Country { get; }
        public decimal BagCharge { get; }
        public int ItemsPerBag => 5;
        public decimal GetBagCost(int items) => (items - (items % ItemsPerBag))/ItemsPerBag * BagCharge;
    }
}
