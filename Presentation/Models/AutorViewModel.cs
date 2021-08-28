using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class AutorViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Nome { get; set; }
        [Required]
        [StringLength(150)]
        public string UltimoNome { get; set; }
        [Required]
        [StringLength(150)]
        public string Nacionalidade { get; set; }
        [Range(minimum:0, maximum: 999)]
        public int QuantidadeLivrosPublicados { get; set; }
        [Required]
        public DateTime Nascimento { get; set; }

        public string NomeCompleto => $"{Id} - {Nome} {UltimoNome}";

        public List<LivroViewModel> Livros { get; set; }
    }
}
