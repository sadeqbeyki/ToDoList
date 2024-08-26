namespace ToDo.Application.DTOs.TaskItems;

public class TaskItemSearchModel
{
    public string? Title { get; set; }
    public bool? IsDone { get; set; }
    public long? TaskListId { get; set; }
}
