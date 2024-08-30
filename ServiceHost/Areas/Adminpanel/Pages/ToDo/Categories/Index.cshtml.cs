using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Application.DTOs.TaskLists;
using ToDo.Application.Interfaces;

namespace ServiceHost.Areas.Adminpanel.Pages.ToDo.Categories;

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

    public PartialViewResult OnGetEdit(long id)
    {
        var taskCategory = _taskCategoryApplication.GetDetails(id);
        return Partial("Edit", taskCategory);
    }
    public IActionResult OnPostEdit(EditTaskListDto command)
    {
        var result = _taskCategoryApplication.Edit(command);
        return new JsonResult(result);
    }
}