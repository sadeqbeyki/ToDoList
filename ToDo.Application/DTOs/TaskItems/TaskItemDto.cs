using System.Collections.Generic;
using ToDo.Application.DTOs.TaskLists;

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
    public List<TaskListDto> TaskList { get; set; } = new List<TaskListDto>();
}

