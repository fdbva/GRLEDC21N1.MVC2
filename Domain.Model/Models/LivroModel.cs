using System;

namespace Domain.Model.Models
{
    public class LivroModel : BaseModel
    {
        public string Titulo { get; set; }
        public string Isbn { get; set; }
        public DateTime Lancamento { get; set; }
        public int QtdPaginas { get; set; }

        public int AutorId { get; set; }
        public AutorModel Autor { get; set; }
    }
}
