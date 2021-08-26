using System.Collections.Generic;

namespace Presentation.Models
{
    public class AutorIndexViewModel
    {
        public string Search { get; set; }
        public bool OrderAscendant { get; set; }
        public IEnumerable<AutorViewModel> Autores { get; set; }
    }
}
