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

        // Get / TodoItem
        // use to get all tasks
        [HttpGet]
        public IEnumerable<TodoItemDto> GetTodoItem()
        {
            var todoItems = repository.GetTodoItem().Select(todoItem => todoItem.AsDto());
            return todoItems;
        }

        // Get / TodoItem / {id}
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
    };
}