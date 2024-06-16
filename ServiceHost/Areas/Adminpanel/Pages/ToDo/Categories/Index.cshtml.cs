using ToDo.Application.Contracts.TaskCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Areas.Adminpanel.Pages.ToDo.Categories;

public class IndexModel : PageModel
{
    public TaskCategorySearchModel SearchModel;
    public List<TaskCategoryViewModel> TaskCategories;
    private readonly ITaskCategoryApplication _taskCategoryApplication;

    public IndexModel(ITaskCategoryApplication taskCategoryApplication)
    {
        _taskCategoryApplication = taskCategoryApplication;
    }

    public void OnGet(TaskCategorySearchModel searchModel)
    {
        TaskCategories = _taskCategoryApplication.Search(searchModel);
    }
    public PartialViewResult OnGetCreate()
    {
        return Partial("./Create", new CreateTaskCategory());
    }

    public JsonResult OnPostCreate(CreateTaskCategory command)
    {
        var result = _taskCategoryApplication.Create(command);
        return new JsonResult(result);
    }

    public PartialViewResult OnGetEdit(long id)
    {
        var taskCategory = _taskCategoryApplication.GetDetails(id);
        return Partial("Edit", taskCategory);
    }
    public IActionResult OnPostEdit(EditTaskCategory command)
    {
        var result = _taskCategoryApplication.Edit(command);
        return new JsonResult(result);
    }
}