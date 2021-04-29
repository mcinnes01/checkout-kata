using Checkout.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkout
{
    public class BagPolicyFactory : IBagPolicyFactory
    {
        private readonly IEnumerable<IBagPolicy> _bagPolicies;

        public BagPolicyFactory(IEnumerable<IBagPolicy> bagPolicies)
        {
            _bagPolicies = bagPolicies;
        }

        public IBagPolicy GetBagPolicy()
        {
            return _bagPolicies
                .Where(b => b.Country.ToString() == Environment.GetEnvironmentVariable("Country"))
                .First();
        }
    }
}
