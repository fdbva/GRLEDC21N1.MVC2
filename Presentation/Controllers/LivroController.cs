﻿using System.Threading.Tasks;
using Application.AppServices;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILivroAppService _livroAppService;
        private readonly IAutorAppService _autorAppService;

        public LivroController(
            ILivroAppService livroAppService,
            IAutorAppService autorAppService)
        {
            _livroAppService = livroAppService;
            _autorAppService = autorAppService;
        }

        // GET: Livro
        public async Task<IActionResult> Index(
            LivroIndexViewModel livroIndexRequest)
        {
            var livroIndexViewModel = new LivroIndexViewModel
            {
                Search = livroIndexRequest.Search,
                OrderAscendant = livroIndexRequest.OrderAscendant,
                Livros = await _livroAppService.GetAllAsync(
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

            var livroViewModel = await _livroAppService.GetByIdAsync(id.Value);

            if (livroViewModel == null)
            {
                return NotFound();
            }

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

            var livroCreated = await _livroAppService.CreateAsync(livroViewModel);

            return RedirectToAction(nameof(Details), new { id = livroCreated.Id });
        }

        private async Task PreencherSelectAutores(int? autorId = null)
        {
            var autores = await _autorAppService.GetAllAsync(true);

            ViewBag.Autores = new SelectList(
                autores,
                nameof(AutorViewModel.Id),
                nameof(AutorViewModel.NomeCompleto),
                autorId);
        }

        // GET: Livro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroViewModel = await _livroAppService.GetByIdAsync(id.Value);
            if (livroViewModel == null)
            {
                return NotFound();
            }

            await PreencherSelectAutores(livroViewModel.AutorId);

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

            try
            {
                await _livroAppService.EditAsync(livroViewModel);
            }
            catch (DbUpdateConcurrencyException) //TODO: Tratamento de erro de banco deve ser feito no Repository
            {
                var exists = await LivroViewModelExistsAsync(livroViewModel.Id);
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

            var livroViewModel = await _livroAppService.GetByIdAsync(id.Value);
            if (livroViewModel == null)
            {
                return NotFound();
            }

            return View(livroViewModel);
        }

        // POST: Livro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _livroAppService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> LivroViewModelExistsAsync(int id)
        {
            var livro = await _livroAppService.GetByIdAsync(id);

            var any = livro != null;

            return any;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsIsbnValid(string isbn, int id)
        {
            return await _livroAppService.IsIsbnValidAsync(isbn, id) 
                ? Json(true)
                : Json($"ISBN {isbn} já está sendo usado.");
        }
    }
}
