using System;
using System.Collections.Generic;

namespace Domain.Model.Models
{
    public class AutorModel : BaseModel
    {
        public string Nome { get; set; }
        public string UltimoNome { get; set; }
        public string Nacionalidade { get; set; }
        public int QuantidadeLivrosPublicados { get; set; }
        public DateTime Nascimento { get; set; }

        public List<LivroModel> Livros { get; set; }
    }
}
