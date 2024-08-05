using AppFramework.Domain;

namespace ToDo.Domain.Entities;

public class TaskItem : EntityBase
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; } = false;
    public long TaskListId { get; private set; }
    public TaskList TaskList { get; private set; }

    public TaskItem(string title, string description, long taskListId)
    {
        Title = title;
        IsDone = false;
        Description = description;  
        TaskListId = taskListId;
    }

    public void MarkDone()
    {
        IsDone = true;
    }

    public void Edit(string title, string description, long taskListId,bool isDone)
    {
        Title = title;  
        Description = description;
        TaskListId = taskListId;
        IsDone = isDone;
    }
}


