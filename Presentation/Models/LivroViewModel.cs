using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Models
{
    public class LivroViewModel
    {
        public int Id { get; set; }
        [StringLength(150)]
        public string Titulo { get; set; }
        [StringLength(maximumLength: 13, MinimumLength = 3)]
        [Remote(action: "IsIsbnValid", controller: "Livro", AdditionalFields = "Id")]
        public string Isbn { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data de Lançamento")]
        public DateTime Lancamento { get; set; }
        public int QtdPaginas { get; set; }

        [Required]
        public int AutorId { get; set; }
        public AutorViewModel Autor { get; set; }
    }
}
