namespace ToDo.Application.DTOs.TaskItems;

public class CreateTaskDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public long TaskListId { get; set; }
}

