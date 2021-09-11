using System.Threading.Tasks;
using Application.AppServices;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers
{
    public class AutorController : Controller
    {
        private readonly IAutorAppService _autorAppService;

        public AutorController(
            IAutorAppService autorAppService)
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

        // GET: Autor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorViewModel = await _autorAppService.GetByIdAsync(id.Value);

            if (autorViewModel == null)
            {
                return NotFound();
            }

            return View(autorViewModel);
        }

        // GET: Autor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AutorViewModel autorViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(autorViewModel);
            }

            var autorCreated = await _autorAppService.CreateAsync(autorViewModel);

            return RedirectToAction(nameof(Details), new { id = autorCreated.Id });
        }

        // GET: Autor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorViewModel = await _autorAppService.GetByIdAsync(id.Value);
            if (autorViewModel == null)
            {
                return NotFound();
            }

            return View(autorViewModel);
        }

        // POST: Autor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AutorViewModel autorViewModel)
        {
            if (id != autorViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(autorViewModel);
            }

            try
            {
                await _autorAppService.EditAsync(autorViewModel);
            }
            catch (DbUpdateConcurrencyException) //TODO: Tratamento de erro de banco deve ser feito no Repository
            {
                var exists = await AutorModelExistsAsync(autorViewModel.Id);
                if (!exists)
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

        // GET: Autor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorViewModel = await _autorAppService.GetByIdAsync(id.Value);
            if (autorViewModel == null)
            {
                return NotFound();
            }

            return View(autorViewModel);
        }

        // POST: Autor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _autorAppService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AutorModelExistsAsync(int id)
        {
            var autor = await _autorAppService.GetByIdAsync(id);

            var any = autor != null;

            return any;
        }
    }
}
