namespace ToDo.Application.DTOs.TaskItem;

public class SearchTaskItemDto
{
    public string Title { get; set; }
    public bool IsDone { get; set; }
    public long TaskListId { get; set; }
}
