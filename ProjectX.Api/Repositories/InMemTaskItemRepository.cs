using ProjectX.Api.Entities;

namespace ProjectX.Api.Repositories
{
    public class InMemTaskItemRepository
    {
        private readonly List<TaskItem> taskitems = new List<TaskItem>()
        {
            new TaskItem { Id = Guid.NewGuid(), Name = "Arkam", Task = "Don't do any thing", TaskDateTime = DateTimeOffset.UtcNow },
            new TaskItem { Id = Guid.NewGuid(), Name = "Brkam", Task = "Do any thing", TaskDateTime = DateTimeOffset.UtcNow },
            new TaskItem { Id = Guid.NewGuid(), Name = "Crkam", Task = "Do Something thing", TaskDateTime = DateTimeOffset.UtcNow },
        };

        public IEnumerable<TaskItem> GetTaskItems()
        {
            return taskitems;
        }

        public TaskItem GetTaskItem(Guid id)
        {
            return taskitems.Where(taskitem => taskitem.Id == id).SingleOrDefault();
        }
    }
}