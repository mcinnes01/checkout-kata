using Checkout.Models;
using System.Collections.Generic;

namespace Checkout.Contracts
{
    public interface IBag
    {
        public IList<BaggedItem> BaggedItems { get; }

        void Add(Product product, int quantity);
    }
}