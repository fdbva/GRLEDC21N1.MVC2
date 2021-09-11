using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels;
using Domain.Model.Interfaces.Services;

namespace Application.AppServices.Implementations
{
    public class LivroAppService : ILivroAppService
    {
        private readonly ILivroService _livroService;

        public LivroAppService(
            ILivroService livroService)
        {
            _livroService = livroService;
        }

        public async Task<IEnumerable<LivroViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            throw new NotImplementedException();
        }

        public async Task<LivroViewModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<LivroViewModel> CreateAsync(LivroViewModel livroViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<LivroViewModel> EditAsync(LivroViewModel livroViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsIsbnValidAsync(string isbn, int id)
        {
            throw new NotImplementedException();
        }
    }
}
