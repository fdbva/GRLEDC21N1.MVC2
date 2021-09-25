using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Services;

namespace Domain.Service.Services
{
    public class IsbnValidator : IIsbnValidator
    {
        public bool Validate(string isbn)
        {
            return isbn.Length switch
            {
                10 => Validate10Digits(isbn),
                13 => Validate13Digits(isbn),
                _ => false
            };
        }

        public bool Validate10Digits(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                return false;
            }

            var digits = isbn.ToCharArray().Select(x => (int)x).ToArray();
            int i, s = 0, t = 0;

            for (i = 0; i < 10; i++)
            {
                t += digits[i];
                s += t;
            }

            var remainder = s % 11;

            var validIsbn = remainder == 0;

            return validIsbn;
        }

        public bool Validate13Digits(string isbn)
        {
            return false;
        }
    }
}
