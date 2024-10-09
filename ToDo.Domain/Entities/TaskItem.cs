using AppFramework.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using ToDo.Domain.Entities.Identity;

namespace ToDo.Domain.Entities;

public class TaskItem : EntityBase
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; } = false;
    public long TaskListId { get; set; }
    public TaskList TaskList { get; private set; }

    public string UserId { get; set; } = string.Empty;

    [ForeignKey("UserId")]
    public ApplicationUser? User { get; set; }

    public TaskItem() { }

    public TaskItem(string title, string description, long taskListId, string userId)
    {
        Title = title;
        IsDone = false;
        Description = description;  
        TaskListId = taskListId;
        UserId = userId;
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


