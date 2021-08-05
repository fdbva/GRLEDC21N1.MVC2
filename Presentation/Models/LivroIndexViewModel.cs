using System.Collections.Generic;
using Domain.Model.Models;

namespace Presentation.Models
{
    public class LivroIndexViewModel
    {
        public string Search { get; set; }
        public bool OrderAscendant { get; set; }
        public IEnumerable<LivroModel> Livros { get; set; }
    }
}
