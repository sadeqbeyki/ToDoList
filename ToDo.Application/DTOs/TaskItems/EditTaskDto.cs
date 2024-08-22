namespace ToDo.Application.DTOs.TaskItems;

public class EditTaskDto : CreateTaskDto
{
    public long Id { get; set; }
    public bool IsDone { get; set; }
}

