using System.Collections.Generic;
using ToDo.Application.Contracts.TaskCategory;

namespace ToDo.Application.Contracts.Task;
public class CreateTask
{
    public string Code { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public List<TaskCategoryViewModel> Categories { get; set; }
}
