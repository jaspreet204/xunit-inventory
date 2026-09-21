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

        [Fact]
        public void ProcessOrder_NotEnoughStock_ReturnsError()
        {
            var product = new Product();
            product.Id = "P2";
            product.Name = "Mouse";
            product.UnitPrice = 50;
            product.StockQuantity = 3;

            _service.AddProduct(product);

            var result = _service.ProcessOrder("P2", 5, 0.10m);
            Assert.False(result.IsSuccess);
            Assert.Equal("Insufficient stock.", result.Message);
        }
        [Fact]
        public void ProcessOrder_FiftyItems_GivesTwentyPercentDiscount()
        {
            var product = new Product();
            product.Id = "P3";
            product.Name = "Keyboard";
            product.UnitPrice = 10;
            product.StockQuantity = 60;

            _service.AddProduct(product);

            var result = _service.ProcessOrder("P3", 50, 0);
            Assert.True(result.IsSuccess);
            Assert.Equal(400, result.TotalCost);
            Assert.Equal(10, product.StockQuantity);
        }
        [Fact]
        public void ProcessOrder_TenItems_GivesTenPercentDiscount()
        {
            var product = new Product();
            product.Id = "P4";
            product.Name = "Headphones";
            product.UnitPrice = 20;
            product.StockQuantity = 20;

            _service.AddProduct(product);

            var result = _service.ProcessOrder("P4", 10, 0);
            Assert.True(result.IsSuccess);
            Assert.Equal(180, result.TotalCost);
        }
    }
}