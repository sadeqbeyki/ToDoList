using AppFramework.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHost.Areas.Adminpanel.Pages.ToDo.Tasks.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Application.DTOs.TaskItems;
using ToDo.Application.DTOs.TaskLists;
using ToDo.Application.Interfaces;

namespace ServiceHost.Areas.Adminpanel.Pages.ToDo.Tasks;

[Area("Adminpanel")]
public class IndexModel : PageModel
{
    public TaskItemViewModel TaskDetails { get; set; } = new();

    public SelectList TaskViewModel;
    public TaskItemSearchModel SearchModel { get; set; } = new();
    public PaginatedList<TaskItemViewModel> TaskItems { get; set; } = [];



    private readonly ITaskService _taskApplication;
    private readonly ITaskListService _taskCategoryApplication;

    public IndexModel(ITaskService taskApplication, ITaskListService taskCategoryApplication)
    {
        _taskApplication = taskApplication;
        _taskCategoryApplication = taskCategoryApplication;
    }
    public async Task OnGetAsync(TaskItemSearchModel searchModel, int pageIndex = 1)
    {
        const int pageSize = 10;
        TaskViewModel = new SelectList(await _taskCategoryApplication.GetAllTaskList(), "Id", "Name");
        TaskItems = await _taskApplication.SearchPaginated(searchModel, pageIndex, pageSize);
        SearchModel = searchModel;
    }

    //public async Task OnGet(TaskItemSearchModel searchModel)
    //{
    //    TaskViewModel = new SelectList(await _taskCategoryApplication.GetAllTaskList(), "Id", "Name");
    //    Tasks = await _taskApplication.Search(searchModel);
    //}


    public async Task<IActionResult> OnGetDetailsAsync(long id)
    {
        TaskDetails = await _taskApplication.GetByIdAsync(id);
        return Partial("Details", TaskDetails);
    }

    public async Task<PartialViewResult> OnGetCreate()
    {
        var viewModel = new CreateTaskViewModel
        {
            TaskLists = await _taskCategoryApplication.GetAllTaskList()
        };
        return Partial("Create", viewModel);
    }

    public async Task<IActionResult> OnPostCreate(CreateTaskViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.TaskLists = await _taskCategoryApplication.GetAllTaskList();
            return Partial("Create", model);
        }

        var result = await _taskApplication.Create(model.Task);

        if (result.IsSucceeded)
            return new JsonResult(result);

        ModelState.AddModelError("", result.Message);
        model.TaskLists = await _taskCategoryApplication.GetAllTaskList();
        return Partial("Create", model);
    }

    public async Task<PartialViewResult> OnGetEdit(long id)
    {
        var viewModel = new EditTaskViewModel
        {
            Task = await _taskApplication.GetByIdAsync(id),
            TaskLists = await _taskCategoryApplication.GetAllTaskList()
        };

        return Partial("Edit", viewModel);
    }

    public async Task<IActionResult> OnPostEdit(EditTaskViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.TaskLists = await _taskCategoryApplication.GetAllTaskList();
            return Partial("Edit", model);
        }

        var dto = new EditTaskDto
        {
            Id = model.Task.Id,
            Title = model.Task.Title,
            IsDone = model.Task.IsDone,
            Description = model.Task.Description,
            TaskListId = model.Task.TaskListId,
        };
        var result = await _taskApplication.Edit(dto);
        if (result.IsSucceeded)
            return new JsonResult(result);

        ModelState.AddModelError("", result.Message);
        model.TaskLists = await _taskCategoryApplication.GetAllTaskList();
        return Partial("Edit", model);
    }

    public async Task<IActionResult> OnPostDelete(long id)
    {
        var result = await _taskApplication.Delete(id);
        return new JsonResult(result);
    }




    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostToggleDoneAsync([FromForm] long id, [FromForm] bool isDone)
    {
        await _taskApplication.ToggleIsDone(id, isDone);
        return new JsonResult(new { success = true });
    }



}