using AppFramework.Domain;
using ToDo.Domain.TaskCategoryAgg;

namespace ToDo.Domain.TaskAgg
{
    public class Task : EntityBase
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Author { get; private set; }
        public string Publisher { get; private set; }
        public string Translator { get; private set; }
        public string Description { get; private set; }
        public long CategoryId { get; private set; }
        public TaskCategory Category { get; private set; }

        public Task(string code, string name, string author, string publisher, string translator, string description, long categoryId)
        {
            Code = code;
            Name = name;
            Author = author;
            Publisher = publisher;
            Translator = translator;
            Description = description;
            CategoryId = categoryId;
        }
        public void Edit(string code, string name, string author, string publisher, string translator, string description, long categoryId)
        {
            Code = code;
            Name = name;
            Author = author;
            Publisher = publisher;
            Translator = translator;
            Description = description;
            CategoryId = categoryId;
        }
    }
}
