using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;  // Add this namespace for ILogger
using Microsoft.EntityFrameworkCore;
using projet_Daber_5edma_version_sans_api.Models;
using System.Diagnostics;
using System.Threading.Tasks;  // Add this namespace for async Task

namespace projet_Daber_5edma_version_sans_api.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;  // Add this field

        public HomeController(ILogger<HomeController> logger, AppDbContext context)  // Add AppDbContext as a parameter
        {
            _logger = logger;
            _context = context;  // Initialize the context
        }

        public async Task<IActionResult> Index()  // Add async Task<>
        {
            var jobOffers = await _context.JobOffers.Include(j => j.Company).ToListAsync();  // Use ToListAsync() instead of ToList()
            return View(jobOffers);  // Pass the jobOffers directly to the view
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
