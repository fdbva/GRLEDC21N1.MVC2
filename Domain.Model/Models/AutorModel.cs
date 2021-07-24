using System;

namespace Domain.Model.Models
{
    public class AutorModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string UltimoNome { get; set; }
        public string Nacionalidade { get; set; }
        public int QuantidadeLivrosPublicados { get; set; }
        public DateTime Nascimento { get; set; }
    }
}
