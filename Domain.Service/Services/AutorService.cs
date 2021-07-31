using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;

namespace Domain.Service.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;

        public AutorService(
            IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<IEnumerable<AutorModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            return await _autorRepository.GetAllAsync(orderAscendant, search);
        }

        public async Task<AutorModel> GetByIdAsync(int id)
        {
            return await _autorRepository.GetByIdAsync(id);
        }

        public async Task<AutorModel> CreateAsync(AutorModel autorModel)
        {
            return await _autorRepository.CreateAsync(autorModel);
        }

        public async Task<AutorModel> EditAsync(AutorModel autorModel)
        {
            return await _autorRepository.EditAsync(autorModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _autorRepository.DeleteAsync(id);
        }
    }
}
