using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using ToDo.Application.DTOs.TaskLists;
using ToDo.Application.Interfaces;

namespace ServiceHost.Areas.Adminpanel.Pages.ToDo.Categories;

public class IndexModel : PageModel
{
    public SearchTaskListDto SearchModel;
    public List<TaskListViewModel> TaskCategories;
    private readonly ITaskListService _taskCategoryApplication;

    public IndexModel(ITaskListService taskCategoryApplication)
    {
        _taskCategoryApplication = taskCategoryApplication;
    }

    public void OnGet(SearchTaskListDto searchModel)
    {
        TaskCategories = _taskCategoryApplication.Search(searchModel);
    }
    public PartialViewResult OnGetCreate()
    {
        return Partial("./Create", new CreateTaskList());
    }

    public JsonResult OnPostCreate(CreateTaskList command)
    {
        var result = _taskCategoryApplication.Create(command);
        return new JsonResult(result);
    }

    public PartialViewResult OnGetEdit(long id)
    {
        var taskCategory = _taskCategoryApplication.GetDetails(id);
        return Partial("Edit", taskCategory);
    }
    public IActionResult OnPostEdit(EditTaskList command)
    {
        var result = _taskCategoryApplication.Edit(command);
        return new JsonResult(result);
    }
}