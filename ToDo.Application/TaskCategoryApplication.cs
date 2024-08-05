using AppFramework.Application;
using System.Collections.Generic;
using ToDo.Application.Contracts.TaskCategory;
using ToDo.Domain.Interfaces;

namespace ToDo.Application
{
    public class TaskCategoryApplication : ITaskCategoryApplication
    {
        private readonly ITaskCategoryRepository _taskCategoryRepository;

        public TaskCategoryApplication(ITaskCategoryRepository taskCategoryRepository)
        {
            _taskCategoryRepository = taskCategoryRepository;
        }

        public OperationResult Create(CreateTaskCategory command)
        {
            var operation = new OperationResult();
            if (_taskCategoryRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var taskCategory = new TaskList(command.Name, command.Description);
            _taskCategoryRepository.Create(taskCategory);
            _taskCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditTaskCategory command)
        {
            var operation = new OperationResult();
            var taskCategory = _taskCategoryRepository.Get(command.Id);
            if (taskCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_taskCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            taskCategory.Edit(command.Name, command.Description);
            _taskCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<TaskCategoryViewModel> GetTaskList()
        {
            return _taskCategoryRepository.GetTaskCategories();
        }

        public EditTaskCategory GetDetails(long id)
        {
            return _taskCategoryRepository.GetDetails(id);
        }


        public List<TaskCategoryViewModel> Search(TaskCategorySearchModel searchModel)
        {
            return _taskCategoryRepository.Search(searchModel);
        }
    }
}
