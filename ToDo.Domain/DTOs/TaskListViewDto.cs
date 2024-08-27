namespace ToDo.Domain.DTOs;

public class TaskListViewDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CreationDate { get; set; }
}
