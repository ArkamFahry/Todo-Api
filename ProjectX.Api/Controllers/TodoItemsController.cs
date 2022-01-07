using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjectX.Api.Dtos;
using ProjectX.Api.Entities;
using ProjectX.Api.Repositories;

namespace ProjectX.Api.Controllers
{
    [ApiController]
    [Route("TodoItems")]
    public class TodoItemsController : ControllerBase
    {
        private readonly IInMemTodoItemRepository repository;

        public TodoItemsController(IInMemTodoItemRepository repository)
        {
            this.repository = repository;
        }

        // Get / TodoItems
        // use to get all tasks
        [HttpGet]
        public async Task<IEnumerable<TodoItemDto>> GetTodoItemsAsync()
        {
            var todoItems = (await repository.GetTodoItemsAsync()).Select(todoItem => todoItem.AsDto());
            return todoItems;
        }

        // Get / TodoItems / {id}
        // use to get a single task
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItemAsync(Guid id)
        {
            var todoitem = await repository.GetTodoItemAsync(id);

            if (todoitem is null)
            {
                return NotFound();
            }

            return todoitem.AsDto();
        }

        // Post / TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> CreateTodoItemAsync(CreateTodoItemDto todoItemDto)
        {
            TodoItem todoItem = new()
            {
                Id = Guid.NewGuid(),
                Name = todoItemDto.Name,
                Todo = todoItemDto.Todo,
                TodoDateTime = DateTimeOffset.UtcNow
            };

            await repository.CreateTodoItemAsync(todoItem);

            return CreatedAtAction(nameof(GetTodoItemAsync), new { id = todoItem.Id }, todoItem.AsDto());
        }

        // Put / TodoItems / {id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTodoItemAsync(Guid id, UpdateTodoItemDto updateTodoItemDto)
        {
            var existingTodoItem = await repository.GetTodoItemAsync(id);

            if (existingTodoItem is null)
            {
                return NotFound();
            }

            TodoItem updatedTodoItem = existingTodoItem with
            {
                Name = updateTodoItemDto.Name,
                Todo = updateTodoItemDto.Todo
            };

            await repository.UpdateTodoItemAsync(updatedTodoItem);

            return NoContent();
        }

        // Delete / TodoItems / {id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodoItemAsync(Guid id)
        {
            var existingTodoItem = await repository.GetTodoItemAsync(id);

            if (existingTodoItem is null)
            {
                return NotFound();
            }

            await repository.DeleteTodoItemAsync(id);

            return NoContent();
        }
    };
}