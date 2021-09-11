using System.Collections.Generic;

namespace Application.ViewModels
{
    public class LivroIndexViewModel
    {
        public string Search { get; set; }
        public bool OrderAscendant { get; set; }
        public IEnumerable<LivroViewModel> Livros { get; set; }
    }
}
