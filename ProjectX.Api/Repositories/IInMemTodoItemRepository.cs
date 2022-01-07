using ProjectX.Api.Entities;

namespace ProjectX.Api.Repositories
{
    public interface IInMemTodoItemRepository
    {
        Task<TodoItem> GetTodoItemAsync(Guid id);
        Task<IEnumerable<TodoItem>> GetTodoItemsAsync();
        Task CreateTodoItemAsync(TodoItem todoItem);
        Task UpdateTodoItemAsync(TodoItem todoItem);
        Task DeleteTodoItemAsync(Guid id);
    }
}