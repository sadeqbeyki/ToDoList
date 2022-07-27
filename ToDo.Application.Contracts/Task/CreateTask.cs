using System.Collections.Generic;
using ToDo.Application.Contracts.TaskCategory;

namespace ToDo.Application.Contracts.Task

{
    public class CreateTask
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Translator { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public List<TaskCategoryViewModel> Categories { get; set; }
    }
}
