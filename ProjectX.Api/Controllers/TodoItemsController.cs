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
        public IEnumerable<TodoItemDto> GetTodoItems()
        {
            var todoItems = repository.GetTodoItems().Select(todoItem => todoItem.AsDto());
            return todoItems;
        }

        // Get / TodoItems / {id}
        // use to get a single task
        [HttpGet("{id}")]
        public ActionResult<TodoItemDto> GetTodoItem(Guid id)
        {
            var todoitem = repository.GetTodoItem(id).AsDto();

            if (todoitem is null)
            {
                return NotFound();
            }

            return todoitem;
        }

        // Post / TodoItems
        [HttpPost]
        public ActionResult<TodoItemDto> CreateTodoItem(CreateTodoItemDto todoItemDto)
        {
            TodoItem todoItem = new()
            {
                Id = Guid.NewGuid(),
                Name = todoItemDto.Name,
                Todo = todoItemDto.Todo,
                TodoDateTime = DateTimeOffset.UtcNow
            };

            repository.CreateTodoItem(todoItem);

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem.AsDto());
        }

        // Put / TodoItems / {id}
        [HttpPut("{id}")]
        public ActionResult UpdateTodoItem(Guid id, UpdateTodoItemDto updateTodoItemDto)
        {
            var existingTodoItem = repository.GetTodoItem(id);

            if (existingTodoItem is null)
            {
                return NotFound();
            }

            TodoItem updatedTodoItem = existingTodoItem with
            {
                Name = updateTodoItemDto.Name,
                Todo = updateTodoItemDto.Todo
            };

            repository.UpdateTodoItem(updatedTodoItem);

            return NoContent();
        }

        // Delete / TodoItems / {id}
        [HttpDelete("{id}")]
        public ActionResult DeleteTodoItem(Guid id)
        {
            var existingTodoItem = repository.GetTodoItem(id);

            if (existingTodoItem is null)
            {
                return NotFound();
            }

            repository.DeleteTodoItem(id);

            return NoContent();
        }
    };
}