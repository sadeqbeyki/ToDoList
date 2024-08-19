using System.Collections.Generic;
using ToDo.Application.DTOs.TaskList;

namespace ToDo.Application.DTOs.TaskItems;


public class TaskItemDto
{
    public long Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public bool IsDone { get; set; }
    public string TaskListTitle { get; set; } = "";
    public long TaskListId { get; set; }
    public string CreationDate { get; set; }
    public List<TaskListViewModel> TaskList { get; set; }
}

public class CreateTaskDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public long TaskListId { get; set; }
}

public class EditTaskDto : CreateTaskDto
{
    public long Id { get; set; }
    public bool IsDone { get; set; }
}

