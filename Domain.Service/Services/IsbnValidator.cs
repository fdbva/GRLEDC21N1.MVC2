using System.Linq;
using Domain.Model.Interfaces.Services;

namespace Domain.Service.Services
{
    public class IsbnValidator : IIsbnValidator
    {
        public const char ValidSeparator = '-';

        public bool Validate(string isbn)
        {//10, 13, 17
            if (isbn.Length != 10 && isbn.Length != 13 && isbn.Length != 17)
            {
                return false;
            }

            switch (isbn.Length)
            {
                case 10:
                {
                    var isDigit = isbn.All(isbnChar => char.IsDigit(isbnChar));

                    if (!isDigit)
                    {
                        return false;
                    }

                    break;
                }
                case 17:
                {
                    var separatorsAtRightPosition =
                        isbn[3] == ValidSeparator &&
                        isbn[5] == ValidSeparator &&
                        isbn[9] == ValidSeparator &&
                        isbn[15] == ValidSeparator;
                    if (!separatorsAtRightPosition)
                    {
                        return false;
                    }

                    break;
                }
                case 13:
                {
                    var isDigit = isbn.All(isbnChar => char.IsDigit(isbnChar));
                    if (!isDigit)
                    {
                        var separatorsAtRightPosition =
                            isbn[1] == ValidSeparator &&
                            isbn[5] == ValidSeparator &&
                            isbn[11] == ValidSeparator;
                        if (!separatorsAtRightPosition)
                        {
                            return false;
                        }
                    }

                    break;
                }
            }

            var parsedIsbn = isbn
                .Replace("-", string.Empty)
                .ToCharArray().Select(x => (int)char.GetNumericValue(x)).ToArray();

            return parsedIsbn.Length switch
            {
                10 => Validate10Digits(parsedIsbn),
                13 => Validate13Digits(parsedIsbn),
                _ => false
            };
        }

        public virtual bool Validate10Digits(int[] isbnDigits)
        {
            if (isbnDigits.Length != 10)
            {
                return false;
            }
            
            int i, s = 0, t = 0;

            for (i = 0; i < 10; i++)
            {
                t += isbnDigits[i];
                s += t;
            }

            var remainder = s % 11;

            var validIsbn = remainder == 0;

            return validIsbn;
        }

        //TODO: finalizar lógica
        public virtual bool Validate13Digits(int[] isbnDigits)
        {
            if (isbnDigits.Length != 13)
            {
                return false;
            }

            return false;
        }
    }
}
