using Checkout.Contracts;
using Checkout.Models;
using System.Collections.Generic;
using Xunit;

namespace Checkout.Tests
{
    public class CheckoutTests
    {
        private readonly ICheckout _checkout;

        public CheckoutTests()
        {
            _checkout = new CheckoutService(new List<Product>
            {
                new Product { Sku = "A99", UnitPrice = 0.5M },
                new Product { Sku = "B15", UnitPrice = 0.3M },
                new Product { Sku = "C40", UnitPrice = 0.6M }
            });
        }

        [Fact]
        public void WhenNoItemsTotalIsZero()
        {
            Assert.Equal(0, _checkout.GetTotal());
        }

        [Theory]
        [InlineData("A99", 1, 0.5)]
        [InlineData("A99", 2, 1.0)]
        [InlineData("B15", 1, 0.3)]
        [InlineData("C40", 1, 0.6)]
        [InlineData("C40", 2, 1.2)]
        [InlineData("C40", 3, 1.8)]
        [InlineData("C40", 4, 2.4)]
        [InlineData("C40", 5, 3.0)]
        public void RequestTotalAndReturnTotal(string sku, int quantity, decimal expected)
        {
            _checkout.Scan(sku, quantity);
            Assert.Equal(expected, _checkout.GetTotal());
        }

        [Fact]
        public void AddCombinationOfProductsAndReturnTotal()
        {
            _checkout.Scan("A99", 2);
            _checkout.Scan("C40", 1);
            _checkout.Scan("C40", 3);
            _checkout.Scan("B15", 1);
            _checkout.Scan("C40", 1);
            Assert.Equal(4.3M, _checkout.GetTotal());
        }
    }
}
