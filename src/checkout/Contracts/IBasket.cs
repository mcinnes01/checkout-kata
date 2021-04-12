using Checkout.Models;
using System.Collections.Generic;

namespace Checkout.Contracts
{
    public interface IBasket
    {
        public IList<BasketItem> Items { get; }
        void Add(Product product, int quantity);
    }
}
