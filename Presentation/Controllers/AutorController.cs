using System.Threading.Tasks;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index()
        {
            //Códigos comentados para demonstrar como visualizar a consulta SQL gerada
            //var sql = _context
            //    .Autores
            //    .OrderBy(x => x.Nome)
            //    .ToQueryString();

            //var sqlComInclude = _context
            //    .Autores
            //    .Include(x => x.Livros)
            //    .OrderBy(x => x.Nome)
            //    .ToQueryString();

            //var exemplo = _context
            //    .Livros
            //    .Where(x => x.Titulo.Length > 1)
            //    .ToQueryString();

            //TODO: Consertar para pegar search da View
            return View(await _autorService.GetAllAsync(true, "lip"));
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

            return View(autorModel);
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
        public async Task<IActionResult> Create(AutorModel autorModel)
        {
            if (!ModelState.IsValid)
            {
                return View(autorModel);
            }

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
            return View(autorModel);
        }

        // POST: Autor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AutorModel autorModel)
        {
            if (id != autorModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            return View(autorModel);
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

            return View(autorModel);
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
