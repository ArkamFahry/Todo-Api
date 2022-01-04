using ProjectX.Api.Entities;

namespace ProjectX.Api.Repositories
{
    public interface IInMemTaskItemRepository
    {
        TaskItem GetTaskItem(Guid id);
        IEnumerable<TaskItem> GetTaskItems();
    }
}