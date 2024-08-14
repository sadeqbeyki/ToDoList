using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Application.Contracts.TaskItem;
using ToDo.Application.Contracts.TaskList;

namespace ServiceHost.Areas.Adminpanel.Pages.ToDo.Tasks;

public class IndexModel : PageModel
{
    [TempData] public string Message { get; set; }
    public List<TaskViewModel> Tasks { get; set; } = new();
    public EditTask TaskDetails { get; set; } = new();
    public TaskSearchModel SearchModel { get; set; } = new();
    public SelectList TaskViewModel;

    private readonly ITaskApplication _taskApplication;
    private readonly ITaskCategoryApplication _taskCategoryApplication;

    public IndexModel(ITaskApplication taskApplication, ITaskCategoryApplication taskCategoryApplication)
    {
        _taskApplication = taskApplication;
        _taskCategoryApplication = taskCategoryApplication;
    }

    public async Task OnGet(TaskSearchModel searchModel)
    {
        //SearchModel = searchModel;  
        TaskViewModel = new SelectList(await _taskCategoryApplication.GetTaskList(), "Id", "Name");
        Tasks = _taskApplication.Search(searchModel);
    }

    public async Task<IActionResult> OnGetDetailsAsync(long id)
    {
        TaskDetails = await _taskApplication.GetDetails(id);
        return Partial("Details", TaskDetails);
    }

    public async Task<PartialViewResult> OnGetCreate()
    {
        var command = new CreateTask
        {
            TaskList = await _taskCategoryApplication.GetTaskList()
        };
        return Partial("Create", command);
    }

    public async Task<IActionResult> OnPostCreate(CreateTask command)
    {
        if (!ModelState.IsValid)
        {
            command.TaskList = await _taskCategoryApplication.GetTaskList();
            return Partial("Create", command);
        }

        var result = await _taskApplication.Create(command);

        if (result.IsSucceeded)
            return new JsonResult(result);

        ModelState.AddModelError("", result.Message);
        command.TaskList = await _taskCategoryApplication.GetTaskList();
        return Partial("Create", command);
    }

    public async Task<PartialViewResult> OnGetEdit(long id)
    {
        var task = await _taskApplication.GetDetails(id);
        task.TaskList = await _taskCategoryApplication.GetTaskList();
        return Partial("Edit", task);
    }

    public async Task<IActionResult> OnPostEdit(EditTask command)
    {
        if (!ModelState.IsValid)
        {
            command.TaskList = await _taskCategoryApplication.GetTaskList();
            return Partial("Update", command);
        }

        var result = await _taskApplication.Edit(command);
        if (result.IsSucceeded)
            return new JsonResult(result);

        ModelState.AddModelError("", result.Message);
        command.TaskList = await _taskCategoryApplication.GetTaskList();
        return Partial("Edit", command);
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostToggleDoneAsync([FromForm] long id, [FromForm] bool isDone)
    {
        await _taskApplication.ToggleIsDone(id, isDone);
        return new JsonResult(new { success = true });
    }



}