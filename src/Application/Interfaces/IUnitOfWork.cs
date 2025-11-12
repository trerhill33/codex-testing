using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Interfaces;

public interface IUnitOfWork
{
    ITodoRepository Todos { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
