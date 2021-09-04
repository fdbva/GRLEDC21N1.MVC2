using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Models;
using Presentation.Services;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEstatisticaHttpService _estatisticaHttpService;

        public HomeController(
            ILogger<HomeController> logger,
            IEstatisticaHttpService estatisticaHttpService)
        {
            _logger = logger;
            _estatisticaHttpService = estatisticaHttpService;
        }
        
        public async Task<IActionResult> Index()
        {
            var homeEstatistica = await _estatisticaHttpService.GetAllAsync();

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
