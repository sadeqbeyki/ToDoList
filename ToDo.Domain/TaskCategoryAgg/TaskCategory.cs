using AppFramework.Domain;
using ToDo.Domain.TaskAgg;
using System.Collections.Generic;

namespace ToDo.Domain.TaskCategoryAgg
{
    public class TaskCategory : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public List<Task> Tasks { get; set; }

        public TaskCategory()
        {
            Tasks = new List<Task>();
        }

        public TaskCategory(string name, string description)
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
}
