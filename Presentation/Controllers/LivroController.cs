using System.Threading.Tasks;
using Application.AppServices;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NonFactors.Mvc.Grid;

namespace Presentation.Controllers
{
    public class LivroController : GridController<LivroViewModel>
    {
        private readonly ILivroAppService _livroAppService;
        private readonly IAutorAppService _autorAppService;

        public LivroController(
            ILivroAppService livroAppService,
            IAutorAppService autorAppService)
            : base (livroAppService)
        {
            _livroAppService = livroAppService;
            _autorAppService = autorAppService;
        }

        // POST: Livro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Create(LivroViewModel livroViewModel)
        {
            if (!ModelState.IsValid)
            {
                await PreencherSelect(livroViewModel.AutorId);
            }

            return await base.Create(livroViewModel);
        }

        protected override async Task PreencherSelect(int? autorId = null)
        {
            var autores = await _autorAppService.GetAllAsync();

            ViewBag.Autores = new SelectList(
                autores,
                nameof(AutorViewModel.Id),
                nameof(AutorViewModel.NomeCompleto),
                autorId);
        }

        // GET: Livro/Edit/5
        public override async Task<IActionResult> Edit(int? id)
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

            await PreencherSelect(livroViewModel.AutorId);

            return View(livroViewModel);
        }

        // POST: Livro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LivroViewModel livroViewModel)
        {
            if (!ModelState.IsValid)
            {
                await PreencherSelect(livroViewModel.AutorId);
            }

            return await base.Edit(id, livroViewModel);
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsIsbnValid(string isbn, int id)
        {
            return await _livroAppService.IsIsbnValidAsync(isbn, id) 
                ? Json(true)
                : Json($"ISBN {isbn} já está sendo usado.");
        }

        protected override void ConfigureGridColumns(IGrid<LivroViewModel> grid)
        {
            grid.Columns.Add(model => model.Titulo);
            grid.Columns.Add(model => model.Isbn);
            grid.Columns.Add(model => model.Lancamento);
            grid.Columns.Add(model => model.QtdPaginas);
            grid.Columns.Add(model => model.Autor.NomeCompleto);
        }
    }
}
