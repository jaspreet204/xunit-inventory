using System;
using Xunit;
using InventorySystem;

namespace Inventory_test
{
    public class Inventory_test
    {
        private readonly InventoryOrderService _service = new();

        [Fact]
        public void ProcessOrder_ValidOrder_ReturnsSuccess()
        {
            var product = new Product();
            product.Id = "P1";
            product.Name = "Laptop";
            product.UnitPrice = 100;
            product.StockQuantity = 20;

            _service.AddProduct(product);

            var result = _service.ProcessOrder("P1", 5, 0.10m);
            Assert.True(result.IsSuccess);
            Assert.Equal(550, result.TotalCost);
            Assert.Equal(15, product.StockQuantity);
        }
        [Fact]
        public void ProcessOrder_ProductDoesNotExist_ReturnsError()
        {
            var result = _service.ProcessOrder("P99", 5, 0.10m);
            Assert.False(result.IsSuccess);
            Assert.Equal("Product not found.", result.Message);
        }
    }
}