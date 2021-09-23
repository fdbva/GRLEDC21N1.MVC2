using System;
using System.Threading.Tasks;
using Application.AppServices;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;

namespace Presentation.Controllers
{
    public abstract class GridController<TViewModel> : CrudController<TViewModel>
        where TViewModel : BaseViewModel
    {
        protected GridController(ICrudAppService<TViewModel> crudAppService) : base(crudAppService)
        {
        }

        // GET: Livro
        public virtual async Task<IActionResult> Index()
        {
            var grid = await CreateExportableGrid();

            return View(grid);
        }

        protected abstract void ConfigureGridColumns(IGrid<TViewModel> grid);

        private async Task<IGrid<TViewModel>> CreateExportableGrid()
        {
            IGrid<TViewModel> grid = new Grid<TViewModel>(await CrudAppService.GetAllAsync());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;

            ConfigureGridColumns(grid);

            // Pager should be excluded on export if all data is needed.
            grid.Pager = new GridPager<TViewModel>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Processors.Add(grid.Sort);
            grid.Pager.RowsPerPage = 6;

            foreach (var column in grid.Columns)
            {
                column.Filter.IsEnabled = true;
                column.Sort.IsEnabled = true;
            }

            return grid;
        }

        [HttpGet]
        public async Task<IActionResult> ExportIndex()
        {
            return Export(await CreateExportableGrid(), $"Exported {DateTime.Now:O}");
        }

        // Using EPPlus from nuget.
        // Export grid method can be reused for all grids.
        private FileContentResult Export(IGrid grid, string fileName)
        {
            var col = 1;
            using ExcelPackage package = new ExcelPackage();
            ExcelWorksheet sheet = package.Workbook.Worksheets.Add("Data");

            foreach (IGridColumn column in grid.Columns)
            {
                sheet.Cells[1, col].Value = column.Title;
                sheet.Column(col++).Width = 18;

                column.IsEncoded = false;
            }

            foreach (IGridRow<object> row in grid.Rows)
            {
                col = 1;

                foreach (IGridColumn column in grid.Columns)
                    sheet.Cells[row.Index + 2, col++].Value = column.ValueFor(row);
            }

            return File(package.GetAsByteArray(), "application/unknown", $"{fileName}.xlsx");
        }
    }
}
