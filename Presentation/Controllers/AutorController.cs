using System.Threading.Tasks;
using Domain.Model.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class AutorController : Controller
    {
        private readonly IAutorService _autorService;

        public AutorController(
            IAutorService autorService)
        {
            _autorService = autorService;
        }

        // GET: Autor
        public async Task<IActionResult> Index(AutorIndexViewModel autorIndexRequest)
        {
            var autorIndexViewModel = new AutorIndexViewModel
            {
                Search = autorIndexRequest.Search,
                OrderAscendant = autorIndexRequest.OrderAscendant,
                Autores = await _autorService.GetAllAsync(
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

            var autorModel = await _autorService.GetByIdAsync(id.Value);

            if (autorModel == null)
            {
                return NotFound();
            }

            var autorViewModel = AutorViewModel.From(autorModel);

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

            var autorModel = autorViewModel.ToModel();
            var autorCreated = await _autorService.CreateAsync(autorModel);

            return RedirectToAction(nameof(Details), new { id = autorCreated.Id });
        }

        // GET: Autor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorModel = await _autorService.GetByIdAsync(id.Value);
            if (autorModel == null)
            {
                return NotFound();
            }

            var autorViewModel = AutorViewModel.From(autorModel);
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

            var autorModel = autorViewModel.ToModel();
            try
            {
                await _autorService.EditAsync(autorModel);
            }
            catch (DbUpdateConcurrencyException) //TODO: Tratamento de erro de banco deve ser feito no Repository
            {
                var exists = await AutorModelExistsAsync(autorModel.Id);
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

            var autorModel = await _autorService.GetByIdAsync(id.Value);
            if (autorModel == null)
            {
                return NotFound();
            }

            var autorViewModel = AutorViewModel.From(autorModel);
            return View(autorViewModel);
        }

        // POST: Autor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _autorService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AutorModelExistsAsync(int id)
        {
            var autor = await _autorService.GetByIdAsync(id);

            var any = autor != null;

            return any;
        }
    }
}
