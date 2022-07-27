using AppFramework.Application;
using ToDo.Application.Contracts.Task;
using System.Collections.Generic;
using ToDo.Domain.TaskAgg;

namespace ToDo.Application
{
    public class TaskApplication : ITaskApplication
    {
        private readonly ITaskRepository _taskRepository;

        public TaskApplication(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public OperationResult Create(CreateTask command)
        {
            var operation = new OperationResult();
            if (_taskRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var task = new Task(command.Code, command.Name, command.Author, command.Publisher, command.Translator, command.Description, command.CategoryId);
            _taskRepository.Create(task);
            _taskRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditTask command)
        {
            var operation = new OperationResult();
            var task = _taskRepository.GetBookWithCategory(command.Id);
            if (task == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_taskRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            task.Edit(command.Code, command.Name, command.Author, command.Publisher, command.Translator, command.Description, command.CategoryId);
            _taskRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<TaskViewModel> GetTasks()
        {
            return _taskRepository.GetTasks();
        }

        public EditTask GetDetails(long id)
        {
            return _taskRepository.GetDetails(id);
        }

        public List<TaskViewModel> Search(TaskSearchModel searchModel)
        {
            return _taskRepository.Search(searchModel);
        }
    }
}
