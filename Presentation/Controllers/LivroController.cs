using System.Threading.Tasks;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILivroService _livroService;
        private readonly IAutorService _autorService;

        public LivroController(
            ILivroService livroService,
            IAutorService autorService)
        {
            _livroService = livroService;
            _autorService = autorService;
        }

        // GET: Livro
        public async Task<IActionResult> Index(
            LivroIndexViewModel livroIndexRequest)
        {
            var livroIndexViewModel = new LivroIndexViewModel
            {
                Search = livroIndexRequest.Search,
                OrderAscendant = livroIndexRequest.OrderAscendant,
                Livros = await _livroService.GetAllAsync(
                    livroIndexRequest.OrderAscendant,
                    livroIndexRequest.Search)
            };
            return View(livroIndexViewModel);
        }

        // GET: Livro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroModel = await _livroService.GetByIdAsync(id.Value);

            if (livroModel == null)
            {
                return NotFound();
            }

            var livroViewModel = LivroViewModel.From(livroModel);
            return View(livroViewModel);
        }

        // GET: Livro/Create
        public async Task<IActionResult> Create()
        {
            await PreencherSelectAutores();

            return View();
        }

        // POST: Livro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LivroViewModel livroViewModel)
        {
            if (!ModelState.IsValid)
            {
                await PreencherSelectAutores(livroViewModel.AutorId);
                return View(livroViewModel);
            }

            var livroModel = livroViewModel.ToModel();
            var livroCreated = await _livroService.CreateAsync(livroModel);

            return RedirectToAction(nameof(Details), new { id = livroCreated.Id });
        }

        private async Task PreencherSelectAutores(int? autorId = null)
        {
            var autores = await _autorService.GetAllAsync(true);

            ViewBag.Autores = new SelectList(
                autores,
                nameof(AutorModel.Id),
                nameof(AutorModel.Nome),
                autorId);
        }

        // GET: Livro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroModel = await _livroService.GetByIdAsync(id.Value);
            if (livroModel == null)
            {
                return NotFound();
            }

            await PreencherSelectAutores(livroModel.AutorId);

            var livroViewModel = LivroViewModel.From(livroModel);
            return View(livroViewModel);
        }

        // POST: Livro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LivroViewModel livroViewModel)
        {
            if (id != livroViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await PreencherSelectAutores(livroViewModel.AutorId);

                return View(livroViewModel);
            }

            var livroModel = livroViewModel.ToModel();
            try
            {
                await _livroService.EditAsync(livroModel);
            }
            catch (DbUpdateConcurrencyException) //TODO: Tratamento de erro de banco deve ser feito no Repository
            {
                var exists = await LivroModelExistsAsync(livroModel.Id);
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

        // GET: Livro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroModel = await _livroService.GetByIdAsync(id.Value);
            if (livroModel == null)
            {
                return NotFound();
            }

            var livroViewModel = LivroViewModel.From(livroModel);
            return View(livroViewModel);
        }

        // POST: Livro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _livroService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> LivroModelExistsAsync(int id)
        {
            var livro = await _livroService.GetByIdAsync(id);

            var any = livro != null;

            return any;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsIsbnValid(string isbn, int id)
        {
            return await _livroService.IsIsbnValidAsync(isbn, id) 
                ? Json(true)
                : Json($"ISBN {isbn} já está sendo usado.");
        }
    }
}
