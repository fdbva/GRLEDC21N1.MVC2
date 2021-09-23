using Application.AppServices;
using Application.ViewModels;
using NonFactors.Mvc.Grid;

namespace Presentation.Controllers
{
    public class AutorController : GridController<AutorViewModel>
    {
        public AutorController(
            IAutorAppService autorAppService)
            : base(autorAppService)
        {
        }

        protected override void ConfigureGridColumns(IGrid<AutorViewModel> grid)
        {
            grid.Columns.Add(model => model.Nome);
            grid.Columns.Add(model => model.UltimoNome);
            grid.Columns.Add(model => model.Nacionalidade);
            grid.Columns.Add(model => model.Nascimento);
        }
    }
}
