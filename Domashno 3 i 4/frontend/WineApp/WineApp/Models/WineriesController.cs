using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using WineApp.Data;

namespace WineApp.Models
{
    public class WineriesController : Controller
    {
        private readonly WineAppContext _context;
        private readonly IHttpClientFactory _httpClientFactory;


        public WineriesController(WineAppContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;

        }

        // GET: Wineries
        public async Task<IActionResult> Index()
        {
            return _context.Winery != null ?
                        View(await _context.Winery.ToListAsync()) :
                        Problem("Entity set 'WineAppContext.Winery'  is null.");
        }

        // GET: Wineries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Winery == null)
            {
                return NotFound();
            }

            var winery = await _context.Winery
                .FirstOrDefaultAsync(m => m.Id == id);
            if (winery == null)
            {
                return NotFound();
            }

            return View(winery);
        }

        // GET: Wineries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wineries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Winery winery)
        {
            string apiUrl = "http://ec2-16-171-8-88.eu-north-1.compute.amazonaws.com/api/Wineries";

            var httpClient = _httpClientFactory.CreateClient();

            var content = new StringContent(winery.ToJson(), System.Text.Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();

                Winery closestWinery = Winery.FromJson(jsonString);

                return View("Details", closestWinery);
            }
            else
            {
                // If the response is not successful, handle the error
                return StatusCode((int)response.StatusCode); // Return the status code as the result
            }
        }

        // POST: Wineries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,lon,lat,name,img,text")] Winery winery)
        {
            if (id != winery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(winery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WineryExists(winery.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(winery);
        }

        // GET: Wineries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Winery == null)
            {
                return NotFound();
            }

            var winery = await _context.Winery
                .FirstOrDefaultAsync(m => m.Id == id);
            if (winery == null)
            {
                return NotFound();
            }

            return View(winery);
        }

        // POST: Wineries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Winery == null)
            {
                return Problem("Entity set 'WineAppContext.Winery'  is null.");
            }
            var winery = await _context.Winery.FindAsync(id);
            if (winery != null)
            {
                _context.Winery.Remove(winery);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WineryExists(int id)
        {
            return (_context.Winery?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
