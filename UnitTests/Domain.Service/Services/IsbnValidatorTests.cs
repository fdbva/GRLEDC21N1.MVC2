using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Domain.Service.Services;
using NSubstitute;
using Xunit;

namespace UnitTests.Domain.Service.Services
{
    public class IsbnValidatorTests
    {
        [Fact]
        public async Task ValidIsbnShouldReturnTrue()
        {
            var livroRepository = Substitute.For<ILivroRepository>();
            livroRepository
                .GetIsbnNotFromThisIdAsync(default, default)
                .ReturnsForAnyArgs((LivroModel)null);

            var livroService = new LivroService(livroRepository);

            var isIsbnValid = await livroService.IsIsbnValidAsync("0306406152", 3);

            Assert.True(isIsbnValid);
        }

        [Fact]
        public async Task InvalidIsbnShouldReturnFalse()
        {
            var livroRepository = Substitute.For<ILivroRepository>();
            livroRepository
                .GetIsbnNotFromThisIdAsync(default, default)
                .ReturnsForAnyArgs((LivroModel)null);

            var livroService = new LivroService(livroRepository);

            var isIsbnValid = await livroService.IsIsbnValidAsync("0306406153", 3);

            Assert.False(isIsbnValid);
        }
    }
}
