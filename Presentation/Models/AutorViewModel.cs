using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Domain.Model.Models;

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

        public List<LivroViewModel> Livros { get; set; }

        public static AutorViewModel From(AutorModel autorModel)
        {
            var autorViewModel = new AutorViewModel
            {
                Id = autorModel.Id,
                Nome = autorModel.Nome,
                UltimoNome = autorModel.UltimoNome,
                Nacionalidade = autorModel.Nacionalidade,
                QuantidadeLivrosPublicados = autorModel.QuantidadeLivrosPublicados,
                Nascimento = autorModel.Nascimento,

                Livros = autorModel?.Livros.Select(x => LivroViewModel.From(x, false)).ToList(),
            };

            return autorViewModel;
        }

        public AutorModel ToModel()
        {
            var autorModel = new AutorModel
            {
                Id = Id,
                Nome = Nome,
                UltimoNome = UltimoNome,
                Nacionalidade = Nacionalidade,
                QuantidadeLivrosPublicados = QuantidadeLivrosPublicados,
                Nascimento = Nascimento,

                Livros = Livros?.Select(x => x.ToModel(false)).ToList(),
            };

            return autorModel;
        }
    }
}
