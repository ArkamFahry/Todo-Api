using ProjectX.Api.Dtos;
using ProjectX.Api.Entities;

namespace ProjectX.Api
{
    public static class Extensions
    {
        public static TodoItemDto AsDto(this TodoItem todoItem)
        {
            return new TodoItemDto
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                Todo = todoItem.Todo,
                TodoDateTime = todoItem.TodoDateTime
            };
        }
    }
}