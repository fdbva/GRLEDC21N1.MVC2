using System;
using System.Threading.Tasks;
using Data.Data;
using Domain.Model.Interfaces.UoW;

namespace Data.UoW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BibliotecaContext _bibliotecaContext;
        private bool _disposed;

        public UnitOfWork(
            BibliotecaContext bibliotecaContext)
        {
            _bibliotecaContext = bibliotecaContext;
        }

        public void BeginTransaction()
        {
            _disposed = false;
        }

        public async Task CommitAsync()
        {
            await _bibliotecaContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _bibliotecaContext?.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
