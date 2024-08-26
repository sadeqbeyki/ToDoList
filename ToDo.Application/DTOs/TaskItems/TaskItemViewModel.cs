using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToDo.Application.DTOs.TaskLists;

namespace ToDo.Application.DTOs.TaskItems;


public class TaskItemViewModel
{
    public long Id { get; set; }
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public bool IsDone { get; set; }
    public string TaskListTitle { get; set; } = "";
    [Display(Name = "Task List")]
    [Range(1, long.MaxValue, ErrorMessage = "Please select a Task List")]
    public long TaskListId { get; set; }
    public string CreationDate { get; set; }
    public List<TaskListDto> TaskList { get; set; } = new List<TaskListDto>();
}

