namespace ToDo.Application.Contracts.Task;

public class TaskViewModel
{
    public long Id { get; set; }
    public string Title { get; set; }
    public bool IsDone { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public long CategoryId { get; set; }
    public string CreationDate { get; set; }
}
