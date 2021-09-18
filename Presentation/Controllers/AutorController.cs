using System.Threading.Tasks;
using Application.AppServices;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers
{
    public class AutorController : CrudController<AutorViewModel>
    {
        private readonly IAutorAppService _autorAppService;

        public AutorController(
            IAutorAppService autorAppService)
            : base(autorAppService)
        {
            _autorAppService = autorAppService;
        }

        // GET: Autor
        public async Task<IActionResult> Index(AutorIndexViewModel autorIndexRequest)
        {
            var autorIndexViewModel = new AutorIndexViewModel
            {
                Search = autorIndexRequest.Search,
                OrderAscendant = autorIndexRequest.OrderAscendant,
                Autores = await _autorAppService.GetAllAsync(
                    autorIndexRequest.OrderAscendant,
                    autorIndexRequest.Search)
            };

            return View(autorIndexViewModel);
        }
    }
}
