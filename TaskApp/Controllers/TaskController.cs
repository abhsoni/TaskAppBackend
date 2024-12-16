using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TaskAppDTO;
using TaskApp.ApiModels;
using TaskAppBL;
using System.Text;
using System.Text.Json;


namespace TaskApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly TasksLogic _logic;
        private readonly IMapper _mapper;
        public TaskController(TasksLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }
    
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var taskList = _logic.GetAllTasks();
            return Ok(new { tasks = taskList });
        }

        [HttpGet("{userId}")]
        public IActionResult GetTasksByUserId(int userId)
        {
            var userTaskList = _logic.GetTaskByUserId(userId);
            return Ok(new { tasks = userTaskList });
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] ApiTaskItem  taskData)
        {
            var taskDto = _mapper.Map<TaskItemDTO>(taskData);
            var task = _logic.AddTask(taskDto);
            if(task == null)
            {
                return BadRequest();
            }
            return Ok(new { success = true, message = "Task added successfully.", taskResult = task });
        }

        [HttpPost("{taskId}")]
        public IActionResult UpdateTask(int taskId)
        {
            var updatedTask = _logic.UpdateTask(taskId);
            if(updatedTask == null)
            {
                return BadRequest();
            }
            return Ok(new { success = true, message = "Task updated successfully.", updateTaskResult=updatedTask });
        }

        [HttpDelete("{taskId}")]
        public IActionResult DeleteTask(int taskId)
        {
            var deletedTask = _logic.DeleteTask(taskId);
            //var task = _context.Tasks.FirstOrDefault(x => x.TaskId == taskId);
            if (deletedTask == null)
            {
                return NotFound(new { message = "Something went wrong. Maybe Task not found." });
            }
            return Ok(new { success = true, message = "Task deleted successfully.",deletedTaskResult=deletedTask });
        }
        [HttpGet("download-excel")]
        public async Task<IActionResult> DownloadExcelFile()
        {
            var taskList = _logic.GetAllTasks();
            if(taskList==null || !taskList.Any())
            {
                return NotFound("No tasks available to download.");
            }
            var csvContent = GenerateCSV(taskList);
            var byteArray = Encoding.UTF8.GetBytes(csvContent);
            var stream = new MemoryStream(byteArray);
            return File(stream, "text/csv", "tasks.csv");
        }
        private string GenerateCSV(List<TaskItemDTO> tasks)
        {
            var sb = new StringBuilder();
            sb.AppendLine("TaskId,Title,Summary,DueDate,IsCompleted,CompletedDate,UserId");
            foreach(var task in tasks)
            {
                sb.AppendLine($"{task.TaskId},{task.Title},{task.Summary},{task.DueDate},{task.IsCompleted},{task.CompletedDate},{task.UserId}");
            }
            return sb.ToString();
        }

    }
}
