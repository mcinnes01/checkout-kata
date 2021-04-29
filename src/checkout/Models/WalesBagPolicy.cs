using Checkout.Contracts;

namespace Checkout.Models
{
    public class WalesBagPolicy : IBagPolicy
    {
        public Country Country => Country.Wales;
        public decimal BagCharge => 0.1M;
    }
}
