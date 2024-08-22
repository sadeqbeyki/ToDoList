using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    //public long Id { get; set; }

    //[Required(ErrorMessage = "Title is required")]
    //public string Title { get; set; }

    //public string Description { get; set; }

    //public bool IsDone { get; set; }

    //[Display(Name = "Task List")]
    //[Range(1, long.MaxValue, ErrorMessage = "Please select a Task List")]
    //public long TaskListId { get; set; }

    //public List<SelectListItem> SelectTaskLists { get; set; } = new();
    public TaskItemDto Task { get; set; } = new();
    public List<TaskListDto> TaskLists { get; set; } = new();
}