namespace ToDo.Domain.Models;

public class TaskItemSearchModel
{
    public string? Title { get; set; }
    public bool? IsDone { get; set; }
    public long? TaskListId { get; set; }
}
