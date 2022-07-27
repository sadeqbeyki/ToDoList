namespace AppQuery.Contracts.Task
{
    public class TaskQueryModel
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

    }
}
