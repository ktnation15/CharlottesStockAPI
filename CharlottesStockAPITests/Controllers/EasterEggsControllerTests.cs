using Microsoft.VisualStudio.TestTools.UnitTesting;
using CharlottesStockAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharlottesStockAPI.Controllers.Tests
{
    [TestClass()]
    public class EasterEggsControllerTests
    {
        private readonly EasterEggsManager _manager;

        public EasterEggsManagerTests()
        {
            _manager = new EasterEggsManager();
        }

        [Fact]
        public void Get_ReturnsAllEasterEggs()
        {
            // Arrange
            var expectedCount = 6;

            // Act
            var result = _manager.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Count);
        }

        [Fact]
        public void GetByProductNo_ReturnsCorrectEasterEgg()
        {
            // Arrange
            var productNo = 8011;

            // Act
            var result = _manager.GetByProductNo(productNo);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productNo, result.ProductNo);
        }

        [Fact]
        public void GetByProductNo_ProductNotFound_ThrowsException()
        {
            // Arrange
            var productNo = 9999;

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => _manager.GetByProductNo(productNo));
        }

        [Fact]
        public void GetLowStock_ReturnsEasterEggsWithLowStock()
        {
            // Arrange
            var stockLevel = 1500;

            // Act
            var result = _manager.GetLowStock(stockLevel);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, egg => Assert.True(egg.InStock <= stockLevel));
        }
    }
}