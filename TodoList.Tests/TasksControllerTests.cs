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
        private readonly Mock<IRepository<TaskDal>> _mockTasksRepo;
        private readonly Mock<IRepository<UserDal>> _mockUsersRepo;
        private readonly TasksController _controller;

        public TasksControllerTests()
        {
            _mockTasksRepo = new Mock<IRepository<TaskDal>>();
            _mockUsersRepo = new Mock<IRepository<UserDal>>();
            _controller = new TasksController(
                _mockTasksRepo.Object,
                _mockUsersRepo.Object);
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
