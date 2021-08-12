using System;
using System.ComponentModel.DataAnnotations;
using Domain.Model.Models;
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

        [Required]
        public int AutorId { get; set; }
        public AutorViewModel Autor { get; set; }

        public static LivroViewModel From(LivroModel livroModel, bool firstMap = true)
        {
            var autor = firstMap //para interromper recursão
                ? AutorViewModel.From(livroModel.Autor)
                : null;

            var livroViewModel = new LivroViewModel
            {
                Id = livroModel.Id,
                Titulo = livroModel.Titulo,
                Isbn = livroModel.Isbn,
                Lancamento = livroModel.Lancamento,
                AutorId = livroModel.AutorId,

                Autor = autor,
            };

            return livroViewModel;
        }

        public LivroModel ToModel(bool firstMap = true)
        {
            var autor = firstMap //para interromper recursão
                ? Autor?.ToModel()
                : null;

            var livroModel = new LivroModel
            {
                Id = Id,
                Titulo = Titulo,
                Isbn = Isbn,
                Lancamento = Lancamento,
                AutorId = AutorId,

                Autor = autor
            };

            return livroModel;
        }
    }
}
