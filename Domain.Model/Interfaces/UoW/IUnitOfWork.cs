using System.Threading.Tasks;

namespace Domain.Model.Interfaces.UoW
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        Task CommitAsync();
    }
}
