using TaskAppDAL.Models;

namespace TaskAppDAL
{
    public class TaskOperations
    {
        private TaskAppDbContext _context;
        public TaskOperations()
        {
            
        }
        public TaskOperations(TaskAppDbContext context) {
            _context = context;
        }
        public virtual List<TaskItem> GetAllTasks()
        {
            return _context.Tasks.ToList();
        }

        public virtual TaskItem? AddTask(TaskItem taskData)
        {
            _context.Tasks.Add(taskData);
            int result = _context.SaveChanges();
            if (result == 0) {
                return null;
            }
            return _context.Tasks.FirstOrDefault(x => x.TaskId == taskData.TaskId);
        }

        public List<TaskItem> GetTaskByUserId(int userId)
        {
            return _context.Tasks.Where(x=>x.UserId == userId).ToList();
        }
        public TaskItem? UpdateTask(int taskId) 
        {
            var updatedTask= _context.Tasks.FirstOrDefault(task => task.TaskId == taskId);
            if (updatedTask == null)
            {
                return null;
            }
            updatedTask.IsCompleted = true;
            _context.SaveChanges();
            return updatedTask;
        }
        public TaskItem? DeleteTask(int taskId) 
        { 
            var deletedTask=_context.Tasks.FirstOrDefault(task=>task.TaskId == taskId);
            if (deletedTask == null) 
            {
                return null;
            }
            _context.Tasks.Remove(deletedTask);
            _context.SaveChanges();
            return deletedTask;
        }
    }
}
