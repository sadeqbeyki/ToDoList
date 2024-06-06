using AppFramework.Domain;

namespace ToDo.Domain.TaskAgg;

public class TaskItem : EntityBase
{
    public string Title { get; set; }
    public bool IsDone { get; set; }

    public TaskItem(string title)
    {
        Title = title;
        IsDone = false;
    }

    public void MarkDone()
    {
        IsDone = true;
    }

    public void Edit(string title)
    {
        Title = Title;  
    }
}


