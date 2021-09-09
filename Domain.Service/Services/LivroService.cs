﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
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
            //exemplo de transaction feito manualmente
            //using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            //var autor = await _autorService.CreateAsync(autorModel);
            var livro = await _livroRepository.CreateAsync(livroModel);
            //lógica xpto

            //transactionScope.Complete();

            return livro;
        }

        public async Task<LivroModel> EditAsync(LivroModel livroModel)
        {
            return await _livroRepository.EditAsync(livroModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _livroRepository.DeleteAsync(id);
        }

        public async Task<bool> IsIsbnValidAsync(string isbn, int id)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                return false;
            }

            var livroModel = await _livroRepository.GetIsbnNotFromThisIdAsync(isbn, id);

            return livroModel == null;
        }
    }
}
