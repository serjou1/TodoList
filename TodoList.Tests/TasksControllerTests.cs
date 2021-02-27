using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using TodoList.API.Controllers;
using TodoList.DAL.Interfaces;
using TodoList.DAL.Models;
using Xunit;

namespace TodoList.Tests
{
    public class TasksControllerTests
    {
        private readonly Mock<IRepository<TaskDal>> _mockRepo;
        private readonly TasksController _controller;

        public TasksControllerTests()
        {
            _mockRepo = new Mock<IRepository<TaskDal>>();
            _controller = new TasksController(_mockRepo.Object);
        }

        [Fact]
        public async Task CreateTask_ActionExecites_ReturnsCreatedAtActionResult()
        {
            var task = new TaskDal();
            var result = await _controller.CreateTask(task);
            Assert.IsType<CreatedAtActionResult>(result);
        }
    }
}
