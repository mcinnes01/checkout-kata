using Checkout.Models;

namespace Checkout.Contracts
{
    public interface IBagPolicyFactory
    {
        public IBagPolicy GetBagPolicy();
    }
}
