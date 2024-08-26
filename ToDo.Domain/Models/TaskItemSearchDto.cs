namespace ToDo.Domain.Models;

public class TaskItemSearchDto
{
    public string? Title { get; set; }
    public bool? IsDone { get; set; }
    public long? TaskListId { get; set; }
}

public class TaskItemDto
{
    public long Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public bool IsDone { get; set; }
    public string TaskListTitle { get; set; } = "";
    public long TaskListId { get; set; }
    public string CreationDate { get; set; }
}
