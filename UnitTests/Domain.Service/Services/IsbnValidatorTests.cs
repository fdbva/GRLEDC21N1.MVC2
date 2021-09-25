using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using Domain.Service.Services;
using NSubstitute;
using Xunit;

namespace UnitTests.Domain.Service.Services
{
    public class IsbnValidatorTests
    {
        //978-0-306-40615-7
        //9780306406157
        [Theory]
        [InlineData("0306406152")]
        [InlineData("9780306406157")]
        public void ValidIsbnShouldReturnTrue(string isbn)
        {
            var isbnValidator = new IsbnValidator();

            var isIsbnValid = isbnValidator.Validate(isbn);

            Assert.True(isIsbnValid);
        }

        [Fact]
        public void Valid10DigitIsbnShouldReturnTrue()
        {
            IIsbnValidator isbnValidator = new IsbnValidator();

            var isIsbnValid = isbnValidator.Validate10Digits("0306406152");

            Assert.True(isIsbnValid);
        }

        [Fact]
        public void Invalid10DigitIsbnShouldReturnFalse()
        {
            IIsbnValidator isbnValidator = new IsbnValidator();

            var isIsbnValid = isbnValidator.Validate10Digits("0306406153");

            Assert.False(isIsbnValid);
        }
    }
}
