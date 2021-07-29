using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Model.Models;
using Data.Data;

namespace Presentation.Controllers
{
    public class AutorController : Controller
    {
        private readonly BibliotecaContext _context;

        public AutorController(BibliotecaContext context)
        {
            _context = context;
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

            return View(await _context.Autores.ToListAsync());
        }

        // GET: Autor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorModel = await _context
                .Autores
                .Include(x => x.Livros)
                .OrderBy(x => x.Nome)
                .FirstOrDefaultAsync(m => m.Id == id);

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
        public async Task<IActionResult> Create([Bind("Id,Nome,UltimoNome,Nacionalidade,QuantidadeLivrosPublicados,Nascimento")] AutorModel autorModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autorModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(autorModel);
        }

        // GET: Autor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorModel = await _context.Autores.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,UltimoNome,Nacionalidade,QuantidadeLivrosPublicados,Nascimento")] AutorModel autorModel)
        {
            if (id != autorModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autorModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorModelExists(autorModel.Id))
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

            var autorModel = await _context.Autores
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var autorModel = await _context.Autores.FindAsync(id);
            _context.Autores.Remove(autorModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutorModelExists(int id)
        {
            return _context.Autores.Any(e => e.Id == id);
        }
    }
}
