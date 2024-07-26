using ToDo.Application.Contracts.Task;
using ToDo.Application.Contracts.TaskCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Adminpanel.Pages.ToDo.Tasks;

public class IndexModel : PageModel
{
    [TempData] public string Message { get; set; }
    public List<TaskViewModel> Tasks { get; set; } = new();
    public TaskViewModel TaskDetails { get; set; } = new();
    public TaskSearchModel SearchModel { get; set; } = new();
    public SelectList TaskViewModel;

    private readonly ITaskApplication _taskApplication;
    private readonly ITaskCategoryApplication _taskCategoryApplication;

    public IndexModel(ITaskApplication taskApplication, ITaskCategoryApplication taskCategoryApplication)
    {
        _taskApplication = taskApplication;
        _taskCategoryApplication = taskCategoryApplication;
    }

    public void OnGet(TaskSearchModel searchModel)
    {
        //SearchModel = searchModel;  
        TaskViewModel = new SelectList(_taskCategoryApplication.GetTaskList(), "Id", "Name");
        Tasks = _taskApplication.Search(searchModel);
    }

    public async Task<IActionResult> OnGetDetailsAsync(long id)
    {
        TaskDetails = await _taskApplication.GetTask(id);
        return Partial("Details", TaskDetails);
    }

    public PartialViewResult OnGetCreate()
    {
        var command = new CreateTask
        {
            TaskList = _taskCategoryApplication.GetTaskList()
        };
        return Partial("Create", command);
    }

    public IActionResult OnPostCreate(CreateTask command)
    {
        if (!ModelState.IsValid)
        {
            command.TaskList = _taskCategoryApplication.GetTaskList();
            return Partial("Create", command);
        }

        var result = _taskApplication.Create(command);

        if (result.IsSucceeded)
            return new JsonResult(result);

        ModelState.AddModelError("", result.Message);
        command.TaskList = _taskCategoryApplication.GetTaskList();
        return Partial("Create", command);
    }

    public PartialViewResult OnGetEdit(long id)
    {
        var task = _taskApplication.GetDetails(id);
        task.TaskList = _taskCategoryApplication.GetTaskList();
        return Partial("Edit", task);
    }

    public JsonResult OnPostEdit(EditTask command)
    {
        var result = _taskApplication.Edit(command);
        return new JsonResult(result);
    }

    public async Task<IActionResult> OnPostToggleDoneAsync([FromForm] long id, [FromForm] bool isDone)
    {
        await _taskApplication.ToggleIsDone(id, isDone);
        return new JsonResult(new { success = true });
    }



}