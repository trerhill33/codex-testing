using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Services;

public class TodoService
{
    private readonly IUnitOfWork _unitOfWork;

    public TodoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IReadOnlyCollection<TodoItem>> GetTodosAsync(CancellationToken cancellationToken = default) =>
        _unitOfWork.Todos.GetAllAsync(cancellationToken);

    public Task<TodoItem?> GetTodoAsync(Guid id, CancellationToken cancellationToken = default) =>
        _unitOfWork.Todos.GetByIdAsync(id, cancellationToken);

    public async Task<TodoItem> CreateTodoAsync(string title, string? description = null, CancellationToken cancellationToken = default)
    {
        var todo = new TodoItem
        {
            Title = title,
            Description = description
        };

        await _unitOfWork.Todos.AddAsync(todo, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return todo;
    }

    public async Task CompleteTodoAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var todo = await _unitOfWork.Todos.GetByIdAsync(id, cancellationToken)
                   ?? throw new KeyNotFoundException($"Todo item with id '{id}' was not found.");

        todo.MarkComplete();
        await _unitOfWork.Todos.UpdateAsync(todo, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteTodoAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.Todos.DeleteAsync(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
