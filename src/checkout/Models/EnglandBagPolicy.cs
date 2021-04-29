using Checkout.Contracts;

namespace Checkout.Models
{
    public class EnglandBagPolicy : IBagPolicy
    {
        public Country Country => Country.England;
        public decimal BagCharge => 0.05M;
    }
}
