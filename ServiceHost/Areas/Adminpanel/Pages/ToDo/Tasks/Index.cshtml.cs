using ToDo.Application.Contracts.Task;
using ToDo.Application.Contracts.TaskCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ServiceHost.Areas.Adminpanel.Pages.ToDo.Tasks;

public class IndexModel : PageModel
{
    [TempData] public string Message { get; set; }
    public TaskSearchModel SearchModel;
    public List<TaskViewModel> Tasks;
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
        TaskViewModel = new SelectList(_taskCategoryApplication.GetTaskList(), "Id", "Name");
        Tasks = _taskApplication.Search(searchModel);
    }
    public PartialViewResult OnGetCreate()
    {
        var command = new CreateTask
        {
            TaskList = _taskCategoryApplication.GetTaskList()
        };
        return Partial("Partial/_Create", command);
    }

    public JsonResult OnPostCreate(CreateTask command)
    {
        var result = _taskApplication.Create(command);
        return new JsonResult(result);
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
}