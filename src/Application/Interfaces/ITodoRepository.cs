using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Interfaces;

public interface ITodoRepository
{
    Task<IReadOnlyCollection<TodoItem>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<TodoItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(TodoItem item, CancellationToken cancellationToken = default);

    Task UpdateAsync(TodoItem item, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
