using System.Collections.Generic;
using ToDo.Application.Contracts.TaskList;

namespace ToDo.Application.Contracts.TaskItem;
public class CreateTask
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public long TaskListId { get; set; }
    public List<TaskCategoryViewModel> TaskList { get; set; }
}
