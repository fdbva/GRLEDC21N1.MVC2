using System;
using System.Threading.Tasks;
using Application.AppServices;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers
{
    public abstract class CrudController<TViewModel> : Controller
        where TViewModel : BaseViewModel
    {
        private readonly ICrudAppService<TViewModel> _crudAppService;

        protected CrudController(
            ICrudAppService<TViewModel> crudAppService)
        {
            _crudAppService = crudAppService;
        }

        public virtual async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _crudAppService.GetByIdAsync(id.Value);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        public virtual async Task<IActionResult> Create()
        {
            await PreencherSelect();

            return View();
        }

        // POST: Autor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Create(TViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                await PreencherSelect();
                return View(viewModel);
            }

            var viewModelCreated = await _crudAppService.CreateAsync(viewModel);

            return RedirectToAction(nameof(Details), new { id = viewModelCreated.Id });
        }

        public virtual async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _crudAppService.GetByIdAsync(id.Value);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(int id, TViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                await _crudAppService.EditAsync(viewModel);
            }
            catch (DbUpdateConcurrencyException) //TODO: Tratamento de erro de banco deve ser feito no Repository
            {
                var exists = await ModelExistsAsync(viewModel.Id);
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

        public virtual async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _crudAppService.GetByIdAsync(id.Value);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _crudAppService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ModelExistsAsync(int id)
        {
            var viewModel = await _crudAppService.GetByIdAsync(id);

            var any = viewModel != null;

            return any;
        }

        protected virtual async Task PreencherSelect(int? autorId = null)
        {

        }
    }
}
