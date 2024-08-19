using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Application.DTOs.TaskItem;
using ToDo.Application.DTOs.TaskItems;
using ToDo.Application.Interfaces;

namespace ServiceHost.Areas.Adminpanel.Pages.ToDo.Tasks;

public class IndexModel : PageModel
{
    [TempData] public string Message { get; set; }
    public List<TaskViewModel> Tasks { get; set; } = new();
    public EditTaskItemDto TaskDetails { get; set; } = new();
    public SearchTaskItemDto SearchModel { get; set; } = new();
    public SelectList TaskViewModel;

    private readonly ITaskApplication _taskApplication;
    private readonly ITaskListService _taskCategoryApplication;

    public IndexModel(ITaskApplication taskApplication, ITaskListService taskCategoryApplication)
    {
        _taskApplication = taskApplication;
        _taskCategoryApplication = taskCategoryApplication;
    }

    public async Task OnGet(SearchTaskItemDto searchModel)
    {
        //SearchModel = searchModel;  
        TaskViewModel = new SelectList(await _taskCategoryApplication.GetAllTaskList(), "Id", "Name");
        Tasks = _taskApplication.Search(searchModel);
    }

    public async Task<IActionResult> OnGetDetailsAsync(long id)
    {
        TaskDetails = await _taskApplication.GetDetails(id);
        return Partial("Details", TaskDetails);
    }

    public async Task<PartialViewResult> OnGetCreate()
    {
        var command = new TaskItemDto
        {
            TaskList = await _taskCategoryApplication.GetAllTaskList()
        };
        return Partial("Create", command);
    }

    public async Task<IActionResult> OnPostCreate(TaskItemDto command)
    {
        if (!ModelState.IsValid)
        {
            command.TaskList = await _taskCategoryApplication.GetAllTaskList();
            return Partial("Create", command);
        }

        var result = await _taskApplication.Create(command);

        if (result.IsSucceeded)
            return new JsonResult(result);

        ModelState.AddModelError("", result.Message);
        command.TaskList = await _taskCategoryApplication.GetAllTaskList();
        return Partial("Create", command);
    }

    public async Task<PartialViewResult> OnGetEdit(long id)
    {
        var task = await _taskApplication.GetDetails(id);
        task.TaskList = await _taskCategoryApplication.GetAllTaskList();
        return Partial("Edit", task);
    }

    public async Task<IActionResult> OnPostEdit(EditTaskItemDto command)
    {
        if (!ModelState.IsValid)
        {
            command.TaskList = await _taskCategoryApplication.GetAllTaskList();
            return Partial("Update", command);
        }

        var result = await _taskApplication.Edit(command);
        if (result.IsSucceeded)
            return new JsonResult(result);

        ModelState.AddModelError("", result.Message);
        command.TaskList = await _taskCategoryApplication.GetAllTaskList();
        return Partial("Edit", command);
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostToggleDoneAsync([FromForm] long id, [FromForm] bool isDone)
    {
        await _taskApplication.ToggleIsDone(id, isDone);
        return new JsonResult(new { success = true });
    }



}