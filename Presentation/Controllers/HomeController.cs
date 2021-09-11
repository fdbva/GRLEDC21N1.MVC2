using System.Diagnostics;
using System.Threading.Tasks;
using Application.AppServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEstatisticaAppService _estatisticaAppService;

        public HomeController(
            ILogger<HomeController> logger,
            IEstatisticaAppService estatisticaAppService)
        {
            _logger = logger;
            _estatisticaAppService = estatisticaAppService;
        }
        
        public async Task<IActionResult> Index()
        {
            var homeEstatistica = await _estatisticaAppService.GetHomeEstatisticaAsync();

            return View(homeEstatistica);
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
