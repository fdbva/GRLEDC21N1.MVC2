using System;
using System.Linq;
using Domain.Model.Interfaces.Services;
using Domain.Service.Services;
using NSubstitute;
using Xunit;

namespace UnitTests.Domain.Service.Services
{
    public class IsbnValidatorTests
    {
        //978-0-306-40615-7
        //9780306406157
        [Fact]
        public void Isbn10DigitsShouldOnlyCallValidate10Digits()
        {
            //Arrange
            var isbnValidator = Substitute.For<IsbnValidator>();
            isbnValidator.Validate10Digits(default).ReturnsForAnyArgs(true);
            isbnValidator.Validate13Digits(default).Returns(false);
            var expectedDigits = new [] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //0-306-40615-2
            //Act
            var isIsbnValid = isbnValidator.Validate("0123456789");

            //Assert
            Assert.True(isIsbnValid);

            isbnValidator
                .Received(1)
                .Validate10Digits(
                    Arg.Is<int[]>(x => x.SequenceEqual(expectedDigits)));
            isbnValidator.DidNotReceiveWithAnyArgs().Validate13Digits(default);
        }

        [Fact]
        public void ValidIsbnWithHifenShouldReturnTrue()
        {
            //Arrange

            //Act

            //Assert
        }

        [Fact]
        public void InvalidIsbnWithHifenShouldReturnFalse()
        {
            //Arrange
            var isbnValidator = Substitute.For<IsbnValidator>();
            isbnValidator.Validate10Digits(default).ReturnsForAnyArgs(true);
            isbnValidator.Validate13Digits(default).ReturnsForAnyArgs(true);

            //Act
            var isIsbnValid = isbnValidator.Validate("0-3064-0615-2");

            //Assert
            isbnValidator.DidNotReceiveWithAnyArgs().Validate10Digits(default);
            isbnValidator.DidNotReceiveWithAnyArgs().Validate13Digits(default);
            Assert.False(isIsbnValid);
        }

        [Theory]
        [InlineData("0-306-40615-2", new[] { 0, 3, 0, 6, 4, 0, 6, 1, 5, 2 })]
        [InlineData("0306406152", new[] { 0, 3, 0, 6, 4, 0, 6, 1, 5, 2 })]
        public void ValidIsbn10ShouldReturnTrue(string isbn, int[] expectedDigits)
        {
            //Arrange
            var isbnValidator = Substitute.For<IsbnValidator>();
            isbnValidator.Validate10Digits(default).ReturnsForAnyArgs(true);

            //Act
            var isIsbnValid = isbnValidator.Validate(isbn);

            //Assert
            Assert.True(isIsbnValid);
            isbnValidator.Received(1).Validate10Digits(
                Arg.Is<int[]>(x => x.SequenceEqual(expectedDigits)));
            isbnValidator.DidNotReceiveWithAnyArgs().Validate13Digits(default);
        }

        [Theory]
        [InlineData("978-0-306-40615-7", new[] { 9, 7, 8, 0, 3, 0, 6, 4, 0, 6, 1, 5, 7 })]
        [InlineData("9780306406157", new[] { 9, 7, 8, 0, 3, 0, 6, 4, 0, 6, 1, 5, 7 })]
        public void ValidIsbn13ShouldReturnTrue(string isbn, int[] expectedDigits)
        {
            //Arrange
            var isbnValidator = Substitute.For<IsbnValidator>();
            isbnValidator.Validate13Digits(default).ReturnsForAnyArgs(true);

            //Act
            var isIsbnValid = isbnValidator.Validate(isbn);

            //Assert
            Assert.True(isIsbnValid);
            isbnValidator.DidNotReceiveWithAnyArgs().Validate10Digits(default);
            isbnValidator.Received(1).Validate13Digits(
                Arg.Is<int[]>(x => x.SequenceEqual(expectedDigits)));
        }

        [Theory]
        [InlineData("978-0-306-4061-57")]
        [InlineData("978-0-3064-061-57")]
        public void InvalidIsbn13HifenShouldReturnFalse(string isbn)
        {
            //Arrange
            var isbnValidator = Substitute.For<IsbnValidator>();

            //Act
            var isIsbnValid = isbnValidator.Validate(isbn);

            //Assert
            Assert.False(isIsbnValid);
            isbnValidator.DidNotReceiveWithAnyArgs().Validate10Digits(default);
            isbnValidator.DidNotReceiveWithAnyArgs().Validate13Digits(default);
        }

        [Fact]
        public void Valid10DigitIsbnShouldReturnTrue()
        {
            //Arrange
            IIsbnValidator isbnValidator = new IsbnValidator();
            var expectedDigits = new[] { 0, 3, 0, 6, 4, 0, 6, 1, 5, 2 };

            //Act
            var isIsbnValid = isbnValidator.Validate10Digits(expectedDigits);

            //Assert
            Assert.True(isIsbnValid);
        }

        [Fact]
        public void Invalid10DigitIsbnShouldReturnFalse()
        {
            //Arrange
            IIsbnValidator isbnValidator = new IsbnValidator();
            var expectedDigits = new[] { 0, 3, 0, 6, 4, 0, 6, 1, 5, 3 };

            //Act
            var isIsbnValid = isbnValidator.Validate10Digits(expectedDigits);

            //Assert
            Assert.False(isIsbnValid);
        }
    }
}
