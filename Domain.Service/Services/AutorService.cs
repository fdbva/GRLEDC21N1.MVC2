using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;

namespace Domain.Service.Services
{
    public class AutorService : CrudService<AutorModel>, IAutorService
    {
        public AutorService(
            IAutorRepository autorRepository) : base(autorRepository)
        {
        }
    }
}
