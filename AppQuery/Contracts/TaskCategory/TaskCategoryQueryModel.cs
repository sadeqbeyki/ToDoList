using AppQuery.Contracts.Task;
using System.Collections.Generic;

namespace AppQuery.Contracts.TaskCategory
{
    public class TaskCategoryQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TaskQueryModel> Tasks { get; set; }
    }
}
