namespace Domain.Model.Interfaces.Services
{
    public interface IIsbnValidator
    {
        bool Validate(string isbn);
        bool Validate10Digits(int[] isbnDigits);
        bool Validate13Digits(int[] isbnDigits);
    }
}
