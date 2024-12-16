using TaskAppDAL;
using TaskAppDTO;
using AutoMapper;
using TaskAppDAL.Models;

namespace TaskAppBL
{
    public class TasksLogic
    {
        private TaskOperations _operations;
        private readonly IMapper _mapper;

        public TasksLogic(TaskOperations operations, IMapper mapper)
        {
            _operations = operations;
            _mapper = mapper;
        }

        public virtual List<TaskItemDTO> GetAllTasks() {
            var tasks = _operations.GetAllTasks();
            var mappedtasks = _mapper.Map<List<TaskItemDTO>>(tasks);
            return mappedtasks;
        }

        public List<TaskItemDTO> GetTaskByUserId(int userId)
        {
            var tasks = _operations.GetTaskByUserId(userId);
            var mappedtasks = _mapper.Map<List<TaskItemDTO>>(tasks);
            return mappedtasks;
        }

        public TaskItem? AddTask(TaskItemDTO item) {
            var task = _mapper.Map<TaskItem>(item);
            return _operations.AddTask(task);
        }
        public TaskItem? UpdateTask(int taskId)
        {
            return _operations.UpdateTask(taskId);
        }
        public TaskItem? DeleteTask(int taskId)
        {
            return _operations.DeleteTask(taskId);
        }
    }
}
