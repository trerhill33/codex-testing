using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;

namespace CleanArchitecture.Infrastructure.Persistence;

public class InMemoryUnitOfWork : IUnitOfWork
{
    public InMemoryUnitOfWork(ITodoRepository todoRepository)
    {
        Todos = todoRepository;
    }

    public ITodoRepository Todos { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult(0);
}
