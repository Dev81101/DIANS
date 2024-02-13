using Microsoft.AspNetCore.Mvc;
using WineryShopMkd.Database;
using WineryShopMkd.Models;

namespace WineryShopMkd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WineriesController : ControllerBase
    {
        public Database1 database;
        public WineryService service;

        public WineriesController()
        {
            database = new Database1();
            service = new WineryService(database.wineryShops);
        }

        // GET: api/Wineries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Winery>>> GetWinery()
        {

            return database.wineryShops;
        }

        // GET: api/Wineries/5

        // POST: api/Wineries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Winery>> PostWinery(Winery winery)
        {
            Winery closestWinery = service.GetClosest(winery);

            return closestWinery;
        }

        // DELETE: api/Wineries/5

    }
}
