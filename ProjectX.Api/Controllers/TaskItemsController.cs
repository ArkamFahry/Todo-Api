using Microsoft.AspNetCore.Mvc;
using ProjectX.Api.Entities;
using ProjectX.Api.Repositories;

namespace ProjectX.Api.Controllers
{
    [ApiController]
    [Route("TaskItems")]
    public class TaskItemsController : ControllerBase
    {
        private readonly InMemTaskItemRepository repository;

        public TaskItemsController()
        {
            repository = new InMemTaskItemRepository();
        }

        // Get / TaskItems
        // use to get all tasks
        [HttpGet]
        public IEnumerable<TaskItem> GetTaskItems()
        {
            var taskitems = repository.GetTaskItems();
            return taskitems;
        }

        // Get / TaskItems / {id}
        // use to get a single task
        [HttpGet("{id}")]
        public TaskItem GetTaskItem(Guid id)
        {
            var taskitem = repository.GetTaskItem(id);
            return taskitem;
        }
    };
}