using ProjectX.Api.Entities;

namespace ProjectX.Api.Repositories
{

    public class InMemTodoItemRepository : IInMemTodoItemRepository
    {
        private readonly List<TodoItem> todoItem = new List<TodoItem>()
        {
            new TodoItem { Id = Guid.NewGuid(), Name = "Arkam", Todo = "Don't do any thing", TodoDateTime = DateTimeOffset.UtcNow },
            new TodoItem { Id = Guid.NewGuid(), Name = "Brkam", Todo = "Do any thing", TodoDateTime = DateTimeOffset.UtcNow },
            new TodoItem { Id = Guid.NewGuid(), Name = "Crkam", Todo = "Do Something thing", TodoDateTime = DateTimeOffset.UtcNow },
        };

        public IEnumerable<TodoItem> GetTodoItem()
        {
            return todoItem;
        }

        public TodoItem GetTodoItem(Guid id)
        {
            return todoItem.Where(TodoItem => TodoItem.Id == id).SingleOrDefault();
        }
    }
}