using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly TodoService _todoService;

    public TodoController(TodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<TodoItem>>> GetAll(CancellationToken cancellationToken)
    {
        var todos = await _todoService.GetTodosAsync(cancellationToken);
        return Ok(todos);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TodoItem>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var todo = await _todoService.GetTodoAsync(id, cancellationToken);
        if (todo is null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    [HttpPost]
    public async Task<ActionResult<TodoItem>> Create(CreateTodoRequest request, CancellationToken cancellationToken)
    {
        var todo = await _todoService.CreateTodoAsync(request.Title, request.Description, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
    }

    [HttpPost("{id:guid}/complete")]
    public async Task<IActionResult> Complete(Guid id, CancellationToken cancellationToken)
    {
        await _todoService.CompleteTodoAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _todoService.DeleteTodoAsync(id, cancellationToken);
        return NoContent();
    }
}

public record CreateTodoRequest(string Title, string? Description);
