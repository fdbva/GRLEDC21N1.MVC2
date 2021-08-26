using System.Collections.Generic;

namespace Presentation.Models
{
    public class LivroIndexViewModel
    {
        public string Search { get; set; }
        public bool OrderAscendant { get; set; }
        public IEnumerable<LivroViewModel> Livros { get; set; }
    }
}
