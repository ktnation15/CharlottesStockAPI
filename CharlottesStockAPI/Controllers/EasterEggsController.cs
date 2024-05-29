using ChocolateLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CharlottesStockAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EasterEggsController : Controller
    {
        private readonly EasterEggsManager _easterEggsManager;
        public EasterEggsController()
        {
            _easterEggsManager = new EasterEggsManager();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get()
        {
            var egg = _easterEggsManager.Get();
            if (egg == null || !egg.Any())
            {
                return NoContent(); // 204 No Content
            }
            return Ok(egg); // 200 OK
        }

        [HttpGet ("{productNo}")]
        public IActionResult GetByProductNo(int productNo)
        {
            try
            {
                var easterEgg = _easterEggsManager.GetByProductNo(productNo);
                return Ok(easterEgg); // 200 OK
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); // 404 Not Found
            }
        }
        [HttpGet("lowstock/{stockLevel}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetLowStock(int stockLevel)
        {
            var egg = _easterEggsManager.GetLowStock(stockLevel);
            if (egg == null || !egg.Any())
            {
                return NotFound(); // 404 Not Found
            }
            return Ok(egg); // 200 OK
        }
        [HttpPut]
        public IActionResult Update([FromBody] EasterEgg easterEgg)
        {
            if(easterEgg == null)
            {
                return BadRequest(); // 400 Bad Request
            }
            try
            {
                _easterEggsManager.Update(easterEgg);
                return NoContent(); // 204 No Content
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); // 404 Not Found
            }
        }
    }
}
