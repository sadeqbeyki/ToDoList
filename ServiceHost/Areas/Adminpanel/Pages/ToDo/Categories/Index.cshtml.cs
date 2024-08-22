using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Application.DTOs.TaskLists;
using ToDo.Application.Interfaces;

namespace ServiceHost.Areas.Adminpanel.Pages.ToDo.Categories;

public class IndexModel : PageModel
{
    public SearchTaskListDto SearchModel;
    public List<TaskListDto> TaskCategories;
    private readonly ITaskListService _taskCategoryApplication;

    public IndexModel(ITaskListService taskCategoryApplication)
    {
        _taskCategoryApplication = taskCategoryApplication;
    }

    public async Task OnGet(SearchTaskListDto searchModel)
    {
        TaskCategories = await _taskCategoryApplication.Search(searchModel);
    }
    public PartialViewResult OnGetCreate()
    {
        return Partial("./Create", new CreateTaskListDto());
    }

    public JsonResult OnPostCreate(CreateTaskListDto command)
    {
        var result = _taskCategoryApplication.Create(command);
        return new JsonResult(result);
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