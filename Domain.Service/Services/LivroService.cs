using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;

namespace Domain.Service.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(
            ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task<IEnumerable<LivroModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            return await _livroRepository.GetAllAsync(orderAscendant, search);
        }

        public async Task<LivroModel> GetByIdAsync(int id)
        {
            return await _livroRepository.GetByIdAsync(id);
        }

        public async Task<LivroModel> CreateAsync(LivroModel livroModel)
        {
            return await _livroRepository.CreateAsync(livroModel);
        }

        public async Task<LivroModel> EditAsync(LivroModel livroModel)
        {
            return await _livroRepository.EditAsync(livroModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _livroRepository.DeleteAsync(id);
        }
    }
}
