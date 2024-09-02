using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Application.DTOs.TaskLists;
using ToDo.Application.Interfaces;

namespace ServiceHost.Areas.Adminpanel.Pages.ToDo.Categories;
[Area("Adminpanel")]

public class IndexModel : PageModel
{
    public TaskListSearchModel SearchModel;
    public List<TaskListViewModel> TaskCategories;
    private readonly ITaskListService _taskCategoryApplication;

    public IndexModel(ITaskListService taskCategoryApplication)
    {
        _taskCategoryApplication = taskCategoryApplication;
    }

    public async Task OnGet(TaskListSearchModel searchModel)
    {
        TaskCategories = await _taskCategoryApplication.SearchAsync(searchModel);
    }
    public PartialViewResult OnGetCreate()
    {
        return Partial("./Create", new CreateTaskListDto());
    }

    public async Task<IActionResult> OnPostCreate(CreateTaskListDto command)
    {
        if (!ModelState.IsValid)
        {
            return Partial("Create", command);
        }

        var result = await _taskCategoryApplication.Create(command);

        if (result.IsSucceeded)
            return new JsonResult(result);

        ModelState.AddModelError("", result.Message);
        return Partial("Create", command);
    }

    public async Task<PartialViewResult> OnGetEdit(long id)
    {
        TaskListViewModel taskCategory = await _taskCategoryApplication.GetDetails(id);
        if (taskCategory == null)
            throw new ArgumentNullException(nameof(taskCategory), $"Task with id {id} not found");

        return Partial("Edit", taskCategory);
    }
    public async Task<IActionResult> OnPostEdit(EditTaskListDto command)
    {
        if (!ModelState.IsValid)
        {
            return Partial("Edit", command);
        }

        var result = await _taskCategoryApplication.Edit(command);
        if (result.IsSucceeded)
            return new JsonResult(result);

        ModelState.AddModelError("", result.Message);
        return Partial("Edit", command);
    }
}