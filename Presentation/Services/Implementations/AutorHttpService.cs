using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Models;

namespace Presentation.Services.Implementations
{
    public class AutorHttpService : IAutorHttpService
    {
        public Task<AutorViewModel> CreateAsync(AutorViewModel autorViewModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AutorViewModel> EditAsync(AutorViewModel autorViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AutorViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            throw new NotImplementedException();
        }

        public Task<AutorViewModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
