using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels;
using AutoMapper;
using Domain.Model.Interfaces.Services;

namespace Application.AppServices.Implementations
{
    public class AutorAppService : IAutorAppService
    {
        private readonly IAutorService _autorService;
        private readonly IMapper _mapper;

        public AutorAppService(
            IAutorService autorService,
            IMapper mapper)
        {
            _autorService = autorService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AutorViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            var autores = await _autorService.GetAllAsync(orderAscendant, search);

            return _mapper.Map<IEnumerable<AutorViewModel>>(autores);
        }

        public async Task<AutorViewModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AutorViewModel> CreateAsync(AutorViewModel autorViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<AutorViewModel> EditAsync(AutorViewModel autorViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
