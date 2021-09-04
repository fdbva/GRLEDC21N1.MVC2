﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class HomeEstatisticaViewModel
    {
        public int QtdLivrosCadastrados { get; set; }
        public int QtdAutoresCadastrados { get; set; }
        public double MediaLivroPorAutor { get; set; }
        public double MediaPaginaPorLivro { get; set; }
        public int QtdPaginasMaiorLivro { get; set; }
        public string AutorMaiorQtdLivros { get; set; }
        public int MaiorQtdLivrosDeUmAutor { get; set; }
        public string AutorMaiorLivro { get; set; }
    }
}
