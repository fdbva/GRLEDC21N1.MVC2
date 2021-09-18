using Application.ViewModels;
using AutoMapper;
using Domain.Model.Interfaces.Services;
using Domain.Model.Interfaces.UoW;
using Domain.Model.Models;

namespace Application.AppServices.Implementations
{
    public class AutorAppService : CrudAppService<AutorModel, AutorViewModel>, IAutorAppService
    {
        public AutorAppService(
            IAutorService autorService,
            IMapper mapper,
            IUnitOfWork unitOfWork) 
            : base(autorService, mapper, unitOfWork)
        {
        }
    }
}
