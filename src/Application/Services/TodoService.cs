using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Services;

public class TodoService
{
    private readonly ITodoRepository _repository;

    public TodoService(ITodoRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyCollection<TodoItem>> GetTodosAsync(CancellationToken cancellationToken = default) =>
        _repository.GetAllAsync(cancellationToken);

    public Task<TodoItem?> GetTodoAsync(Guid id, CancellationToken cancellationToken = default) =>
        _repository.GetByIdAsync(id, cancellationToken);

    public async Task<TodoItem> CreateTodoAsync(string title, string? description = null, CancellationToken cancellationToken = default)
    {
        var todo = new TodoItem
        {
            Title = title,
            Description = description
        };

        await _repository.AddAsync(todo, cancellationToken);
        return todo;
    }

    public async Task CompleteTodoAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var todo = await _repository.GetByIdAsync(id, cancellationToken)
                   ?? throw new KeyNotFoundException($"Todo item with id '{id}' was not found.");

        todo.MarkComplete();
        await _repository.UpdateAsync(todo, cancellationToken);
    }

    public Task DeleteTodoAsync(Guid id, CancellationToken cancellationToken = default) =>
        _repository.DeleteAsync(id, cancellationToken);
}
