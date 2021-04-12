using Checkout.Contracts;
using Checkout.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkout
{
    public class CheckoutService : ICheckout
    {
        private readonly IEnumerable<Product> _products;
        private readonly IBasket _basket;

        public CheckoutService(IEnumerable<Product> products)
        {
            _products = products;
            _basket = new Basket();
        }

        public void Scan(string sku, int quantity)
        {
            throw new NotImplementedException();
        }

        public decimal GetTotal()
        {
            throw new NotImplementedException();
        }
    }
}
