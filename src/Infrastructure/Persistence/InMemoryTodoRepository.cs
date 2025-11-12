using System.Collections.Concurrent;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Infrastructure.Persistence;

public class InMemoryTodoRepository : ITodoRepository
{
    private readonly ConcurrentDictionary<Guid, TodoItem> _store = new();

    public Task AddAsync(TodoItem item, CancellationToken cancellationToken = default)
    {
        _store[item.Id] = item;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _store.TryRemove(id, out _);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<TodoItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<TodoItem> result = _store.Values.ToList().AsReadOnly();
        return Task.FromResult(result);
    }

    public Task<TodoItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _store.TryGetValue(id, out var item);
        return Task.FromResult(item);
    }

    public Task UpdateAsync(TodoItem item, CancellationToken cancellationToken = default)
    {
        _store[item.Id] = item;
        return Task.CompletedTask;
    }
}
