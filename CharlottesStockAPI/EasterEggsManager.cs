using ChocolateLib;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace CharlottesStockAPI
{
    public class EasterEggsManager
    {
        private int _nextProductNo = 4;
        private static List<EasterEgg> easterEgg = new List<EasterEgg>() // List of Easter Eggs
        {
            new EasterEgg { ProductNo = 8011, ChocolateType = "mørk", Price = 28, InStock = 5012 },
            new EasterEgg { ProductNo = 8012, ChocolateType = "mørk", Price = 32, InStock = 3420 },
            new EasterEgg { ProductNo = 8013, ChocolateType = "mørk", Price = 46, InStock = 1180 },
            new EasterEgg { ProductNo = 8022, ChocolateType = "lys", Price = 31, InStock = 2870 },
            new EasterEgg { ProductNo = 8023, ChocolateType = "lys", Price = 41, InStock = 1067 },
            new EasterEgg { ProductNo = 8032, ChocolateType = "hvid", Price = 34, InStock = 2017 }
        };

        public List<EasterEgg> Get()
        {
            return easterEgg;
        }
        public EasterEgg? GetByProductNo(int productNo)
        {
            var egg = easterEgg.FirstOrDefault(e => e.ProductNo == productNo);
            if (egg == null)
            {
                throw new KeyNotFoundException("Easter Egg not found, ask the bunny");
            }
            return egg;
        }
        public List<EasterEgg> GetLowStock(int stockLevel)
        {
            return easterEgg.Where(e => e.InStock < stockLevel).ToList();
        }
        public void Update(EasterEgg egg)
        {
            var existingEasterEgg = easterEgg.FirstOrDefault(e => e.ProductNo == egg.ProductNo);
            if (existingEasterEgg == null)
            {
                throw new KeyNotFoundException("Easter Egg not found, ask the bunny");
            }
            existingEasterEgg.ChocolateType = egg.ChocolateType;
            existingEasterEgg.InStock = egg.InStock;
            existingEasterEgg.Price = egg.Price;
        }
    }
}
