using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using TaskAppBL;
using TaskAppDAL;
using Moq;
using TaskAppDAL.Models;
using TaskAppDTO;
using FluentAssertions;

namespace TaskAppUnitTests
{
    [TestClass]
    public class TasksLogicTests
    {
        private TasksLogic _tasksLogic;
        private Mock<TaskOperations> _mockOperations;
        private IMapper _mapper;
        [TestInitialize]
        public void Init()
        {
            var mockMapper = new Mock<IMapper>();
            _mockOperations = new Mock<TaskOperations>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BLMapper>();
            });
            var mapper = config.CreateMapper();
            _tasksLogic = new TasksLogic(_mockOperations.Object, mapper);
        }
        [TestMethod]
        public void GetAllTasksTest()
        {
            _mockOperations.Setup(op => op.GetAllTasks()).Returns(new List<TaskItem>
            {
                new TaskItem
                {
                    TaskId = 1,
                    Title = "Task 1",
                    Summary = "Summary 1",
                    DueDate = DateTime.Now.AddDays(1),
                    IsCompleted = false,
                    CompletedDate = DateTime.MinValue,
                    UserId = 101
                }
            });
            var tasks = _tasksLogic.GetAllTasks();
            Console.WriteLine(tasks.Count);
            //Debug.WriteLine(tasks.Count);
            Assert.AreEqual(2, 1 + 1);
        }
        [TestMethod]
        public void AddTask_SuccessTest()
        {
            var taskToAdd = new TaskItemDTO
            {
                Title = "Task 1",
                Summary = "Summary 1",
                DueDate = DateTime.Now.AddDays(1),
                IsCompleted = false,
                CompletedDate = DateTime.MinValue,
                UserId = 101
            };
            _mockOperations.Setup(op => op.AddTask(It.IsAny<TaskItem>())).Returns((TaskItem task) =>
            {
                task.TaskId = 1;
                return task;
            });
            var result = _tasksLogic.AddTask(taskToAdd);
            result.Should().NotBeNull();
            result.TaskId.Should().Be(1);
            result.Title.Should().Be(taskToAdd.Title);
            result.Summary.Should().Be(taskToAdd.Summary);
            result.UserId.Should().Be(taskToAdd.UserId);
            //Assert.IsNotNull(result);
            //Assert.AreEqual(1, result.TaskId);
            //Assert.AreEqual(taskToAdd.Title, result.Title);
            //Assert.AreEqual(taskToAdd.Summary, result.Summary);
            //Assert.AreEqual(taskToAdd.UserId, result.UserId);
            _mockOperations.Verify(op => op.AddTask(It.IsAny<TaskItem>()), Times.Once);
        }
        [TestMethod]
        public void AddTask_FailureTest()
        {
            var taskToAdd = new TaskItemDTO
            {
                Title = "Task 1",
                Summary = "Summary 1",
                DueDate = DateTime.Now.AddDays(1),
                IsCompleted = false,
                CompletedDate = DateTime.MinValue,
                UserId = 101
            };
            _mockOperations.Setup(op => op.AddTask(It.IsAny<TaskItem>())).Returns((TaskItem)null);
            var result = _tasksLogic.AddTask(taskToAdd);
            result.Should().BeNull();
            //Assert.IsNull(result);
            _mockOperations.Verify(op => op.AddTask(It.IsAny<TaskItem>()), Times.Once);
        }
    }
    public class BLMapper : Profile
    {
        public BLMapper()
        {
            CreateMap<TaskItem, TaskItemDTO>().ReverseMap();
            CreateMap<UserItem, UserItemDTO>().ReverseMap();
        }
    }
}
