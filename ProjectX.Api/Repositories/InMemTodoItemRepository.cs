using ProjectX.Api.Entities;

namespace ProjectX.Api.Repositories
{

    public class InMemTodoItemRepository : IInMemTodoItemRepository
    {
        private readonly List<TodoItem> todoItems = new List<TodoItem>()
        {
            new TodoItem { Id = Guid.NewGuid(), Name = "Arkam", Todo = "Don't do any thing", TodoDateTime = DateTimeOffset.UtcNow },
            new TodoItem { Id = Guid.NewGuid(), Name = "Brkam", Todo = "Do any thing", TodoDateTime = DateTimeOffset.UtcNow },
            new TodoItem { Id = Guid.NewGuid(), Name = "Crkam", Todo = "Do Something thing", TodoDateTime = DateTimeOffset.UtcNow },
        };

        // gets one specifice items
        public TodoItem GetTodoItem(Guid id)
        {
            return todoItems.Where(TodoItem => TodoItem.Id == id).SingleOrDefault();
        }

        // gets all the items
        public IEnumerable<TodoItem> GetTodoItems()
        {
            return todoItems;
        }

        //creates a single item
        public void CreateTodoItem(TodoItem todoItem)
        {
            todoItems.Add(todoItem);
        }

        public void UpdateTodoItem(TodoItem todoItem)
        {
            var index = todoItems.FindIndex(existingTodoItem => existingTodoItem.Id == todoItem.Id);
            todoItems[index] = todoItem;
        }

        public void DeleteTodoItem(Guid id)
        {
            var index = todoItems.FindIndex(existingTodoItem => existingTodoItem.Id == id);
            todoItems.RemoveAt(index);
        }
    }
}