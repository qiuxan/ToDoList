using Microsoft.AspNetCore.Mvc;
using ToDoList.Api.Contracts;
using ToDoList.Application.Common;
using ToDoList.Application.Todos;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/todos")]
public sealed class TodosController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<TodoResponse>> Create(
        CreateTodoRequest request,
        CreateTodoUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(
            new CreateTodoCommand(request.Title, request.Description),
            cancellationToken);

        if (result.Status == ResultStatus.ValidationError)
        {
            return BadRequest(result.Errors);
        }

        var response = TodoResponse.FromTodoDto(result.Value!);

        return CreatedAtAction(
            nameof(GetById),
            new { id = response.Id },
            response);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<TodoResponse>>> GetAll(
        GetTodosUseCase useCase,
        CancellationToken cancellationToken)
    {
        var todos = await useCase.ExecuteAsync(cancellationToken);

        return Ok(todos.Select(TodoResponse.FromTodoDto).ToArray());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TodoResponse>> GetById(
        Guid id,
        GetTodoByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(id, cancellationToken);

        if (result.Status == ResultStatus.NotFound)
        {
            return NotFound();
        }

        return Ok(TodoResponse.FromTodoDto(result.Value!));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TodoResponse>> Update(
        Guid id,
        UpdateTodoRequest request,
        UpdateTodoUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(
            id,
            new UpdateTodoCommand(request.Title, request.Description),
            cancellationToken);

        return result.Status switch
        {
            ResultStatus.NotFound => NotFound(),
            ResultStatus.ValidationError => BadRequest(result.Errors),
            _ => Ok(TodoResponse.FromTodoDto(result.Value!))
        };
    }

    [HttpPatch("{id:guid}/complete")]
    public async Task<IActionResult> Complete(
        Guid id,
        MarkTodoCompleteUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(id, cancellationToken);

        return result.Status == ResultStatus.NotFound
            ? NotFound()
            : NoContent();
    }

    [HttpPatch("{id:guid}/incomplete")]
    public async Task<IActionResult> Incomplete(
        Guid id,
        MarkTodoIncompleteUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(id, cancellationToken);

        return result.Status == ResultStatus.NotFound
            ? NotFound()
            : NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        DeleteTodoUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(id, cancellationToken);

        return result.Status == ResultStatus.NotFound
            ? NotFound()
            : NoContent();
    }
}