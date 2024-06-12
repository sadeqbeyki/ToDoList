using AppFramework.Domain;
using ToDo.Domain.TaskCategoryAgg;

namespace ToDo.Domain.TaskAgg;

public class TaskItem : EntityBase
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public long CategoryId { get; private set; }
    public TaskCategory Category { get; private set; }

    public TaskItem(string title, string description, long categoryId)
    {
        Title = title;
        IsDone = false;
        Description = description;  
        CategoryId = categoryId;
    }

    public void MarkDone()
    {
        IsDone = true;
    }

    public void Edit(string title, string description, long categoryId)
    {
        Title = title;  
        Description = description;
        CategoryId = categoryId;
    }
}


