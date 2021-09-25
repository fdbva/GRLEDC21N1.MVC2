namespace Domain.Model.Interfaces.Services
{
    public interface IIsbnValidator
    {
        bool Validate(string isbn);
        bool Validate10Digits(string isbn);
        bool Validate13Digits(string isbn);
    }
}
