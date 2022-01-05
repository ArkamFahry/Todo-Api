using ProjectX.Api.Entities;

namespace ProjectX.Api.Repositories
{
    public interface IInMemTodoItemRepository
    {
        TodoItem GetTodoItem(Guid id);
        IEnumerable<TodoItem> GetTodoItems();
        void CreateTodoItem(TodoItem todoItem);
        void UpdateTodoItem(TodoItem todoItem);
        void DeleteTodoItem(Guid id);
    }
}