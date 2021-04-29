using Checkout.Contracts;
using Checkout.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Checkout.Tests
{
    public class CheckoutTests
    {
        private readonly ICheckout _checkout;

        public CheckoutTests()
        {
            _checkout = new CheckoutService(
                new List<Product>
                {
                    new Product { Sku = "A99", UnitPrice = 0.5M },
                    new Product { Sku = "B15", UnitPrice = 0.3M },
                    new Product { Sku = "C40", UnitPrice = 0.6M }
                },
                new List<IDiscount>
                {
                    new Discount { Sku = "A99", Quantity = 3, OfferPrice = 1.3M },
                    new Discount { Sku = "B15", Quantity = 2, OfferPrice = 0.45M }
                },
                new BagPolicyFactory(new List<IBagPolicy>
                {
                    new EnglandBagPolicy(),
                    new WalesBagPolicy()
                }));
        }

        [Fact]
        public void WhenNoItemsTotalIsZero()
        {
            Environment.SetEnvironmentVariable("Country", "England");
            Assert.Equal(0, _checkout.GetTotal());
        }

        [Theory]
        [InlineData("A99", 1, 0.5)]
        [InlineData("A99", 2, 1.0)]
        [InlineData("A99", 3, 1.3)]
        [InlineData("A99", 4, 1.8)]
        [InlineData("A99", 5, 2.35)]
        [InlineData("B15", 1, 0.3)]
        [InlineData("B15", 2, 0.45)]
        [InlineData("B15", 3, 0.75)]
        [InlineData("B15", 4, 0.9)]
        [InlineData("B15", 5, 1.25)]
        [InlineData("C40", 1, 0.6)]
        [InlineData("C40", 2, 1.2)]
        [InlineData("C40", 3, 1.8)]
        [InlineData("C40", 4, 2.4)]
        [InlineData("C40", 5, 3.05)]
        public void RequestTotalAndReturnTotal(string sku, int quantity, decimal expected)
        {
            Environment.SetEnvironmentVariable("Country", "England");
            _checkout.Scan(sku, quantity);
            Assert.Equal(expected, _checkout.GetTotal());
        }

        [Fact]
        public void AddCombinationOfProductsAndReturnTotal()
        {
            Environment.SetEnvironmentVariable("Country", "England");
            _checkout.Scan("A99", 2);
            _checkout.Scan("C40", 1);
            _checkout.Scan("C40", 3);
            _checkout.Scan("B15", 4);
            _checkout.Scan("C40", 1);
            _checkout.Scan("B15", 1);
            _checkout.Scan("A99", 3);
            Assert.Equal(6.65M, _checkout.GetTotal());
        }

        [Fact]
        public void BiscuitsAppleBiscuitsAppliesBiscuitDiscount()
        {
            Environment.SetEnvironmentVariable("Country", "England");
            _checkout.Scan("B15", 1);
            _checkout.Scan("A99", 1);
            _checkout.Scan("B15", 1);
            Assert.Equal(0.95M, _checkout.GetTotal());
        }

        [Fact]
        public void FiveItemsIncursFivePenceBagChargeInEngland()
        {
            Environment.SetEnvironmentVariable("Country", "England");
            _checkout.Scan("C40", 5);
            Assert.Equal(3.05M, _checkout.GetTotal());
        }

        [Fact]
        public void FiveItemsIncursTenPenceBagChargeInWales()
        {
            Environment.SetEnvironmentVariable("Country", "Wales");
            _checkout.Scan("C40", 5);
            Assert.Equal(3.1M, _checkout.GetTotal());
        }
    }
}
