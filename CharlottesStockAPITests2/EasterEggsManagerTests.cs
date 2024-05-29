using Microsoft.VisualStudio.TestTools.UnitTesting;
using CharlottesStockAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChocolateLib;

namespace CharlottesStockAPI.Tests
{
    [TestClass]
    public class EasterEggsManagerTests
    {
        private EasterEggsManager _manager;

        [TestInitialize]
        public void Setup()
        {
            _manager = new EasterEggsManager();
        }
        [TestMethod]
        public void GetTest()// Test af metoden Get
        {
            // Arrange & Act
            var result = _manager.Get();

            // Assert
            Assert.AreEqual(6, result.Count); // Antallet af initialiserede objekter i listen
        }

        [TestMethod]
        public void GetByProductNoTest()// Test af metoden GetByProductNo
        {
            // Arrange
            var productNo = 8011;

            // Act
            var result = _manager.GetByProductNo(productNo);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(productNo, result.ProductNo);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetByProductNo_ProductNoNotFound_ThrowsException()// Test af metoden GetByProductNo
        {
            // Arrange
            var productNo = 123;

            // Act
            _manager.GetByProductNo(productNo);

            // Assert handled by ExpectedException
        }

        [TestMethod]
        public void GetLowStockTest()// Test af metoden GetLowStock
        {
            // Arrange
            var stockLevel = 3;

            // Act
            var result = _manager.GetLowStock(stockLevel);

            // Assert
            Assert.IsTrue(result.All(e => e.InStock <= stockLevel));
        }

        [TestMethod]
        public void UpdateTest()// Test af opdatering af et produkt
        {
            // Arrange
            var updatedEgg = new EasterEgg { ProductNo = 8012, ChocolateType = "mørk", Price = 30, InStock = 44 };

            // Act
            _manager.Update(updatedEgg);
            var result = _manager.GetByProductNo(8012);

            // Assert
            Assert.AreEqual(updatedEgg.Price, result.Price);
            Assert.AreEqual(updatedEgg.InStock, result.InStock);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Update_ProductNoNotFound_ThrowsException()
        {
            // Arrange
            var updatedEgg = new EasterEgg { ProductNo = 8025, ChocolateType = "mørk", Price = 30, InStock = 32 };

            // Act
            _manager.Update(updatedEgg);

            // Assert handled by ExpectedException
        }
    }
}