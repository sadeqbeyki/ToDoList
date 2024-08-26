using System.Collections.Generic;
using ToDo.Application.DTOs.TaskItems;
using ToDo.Application.DTOs.TaskLists;

namespace ServiceHost.Areas.Adminpanel.Pages.ToDo.Tasks.ViewModels;

public class CreateTaskViewModel
{
    public CreateTaskDto Task { get; set; } = new();
    public List<TaskListDto> TaskLists { get; set; } = new();
}

public class EditTaskViewModel
{
    public TaskItemViewModel Task { get; set; } = new();
    public List<TaskListDto> TaskLists { get; set; } = new();
}