using System.Collections.Generic;
using ToDo.Application.Contracts.TaskCategory;

namespace ToDo.Application.Contracts.Task;
public class CreateTask
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public long CategoryId { get; set; }
    public List<TaskCategoryViewModel> Categories { get; set; }
}
