using AppFramework.Domain;
using System.Collections.Generic;

namespace ToDo.Domain.Entities;

public class TaskList : EntityBase
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public List<TaskItem> TaskItems { get; set; }

    public TaskList()
    {
        TaskItems = new List<TaskItem>();
    }

    public TaskList(string name, string description)
    {
        Name = name;
        Description = description;
    }
    public void Edit(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
