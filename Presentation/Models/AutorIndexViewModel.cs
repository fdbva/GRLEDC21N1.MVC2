using System.Collections.Generic;
using Domain.Model.Models;

namespace Presentation.Models
{
    public class AutorIndexViewModel
    {
        public string Search { get; set; }
        public bool OrderAscendant { get; set; }
        public IEnumerable<AutorModel> Autores { get; set; }
    }
}
