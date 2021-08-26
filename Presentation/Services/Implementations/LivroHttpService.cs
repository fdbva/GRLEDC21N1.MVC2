using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Models;

namespace Presentation.Services.Implementations
{
    public class LivroHttpService : ILivroHttpService
    {
        public Task<LivroViewModel> CreateAsync(LivroViewModel livroViewModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<LivroViewModel> EditAsync(LivroViewModel livroViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LivroViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            throw new NotImplementedException();
        }

        public Task<LivroViewModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsIsbnValidAsync(string isbn, int id)
        {
            throw new NotImplementedException();
        }
    }
}
