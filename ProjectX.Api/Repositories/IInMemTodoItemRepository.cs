using ProjectX.Api.Entities;

namespace ProjectX.Api.Repositories
{
    public interface IInMemTodoItemRepository
    {
        Task<TodoItem> GetTodoItem(Guid id);
        Task<IEnumerable<TodoItem>> GetTodoItemsAsync();
        Task<IEnumerable<TodoItem>> GetTodoSearchItemsAsync(String s);
        Task CreateTodoItemAsync(TodoItem todoItem);
        Task UpdateTodoItemAsync(TodoItem todoItem);
        Task DeleteTodoItemAsync(Guid id);
    }
}