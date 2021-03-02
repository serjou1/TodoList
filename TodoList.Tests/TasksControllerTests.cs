using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
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

        private readonly List<TaskDal> _testTasks;

        public TasksControllerTests()
        {
            _mockTasksRepo = new Mock<IRepository<TaskDal>>();
            _mockUsersRepo = new Mock<IRepository<UserDal>>();
            _controller = new TasksController(
                _mockTasksRepo.Object,
                _mockUsersRepo.Object);

            _testTasks = new List<TaskDal>();
            SetTestTasks();
        }

        // todo add tests

        [Fact]
        public async Task CreateTask_ActionExecites_ReturnsCreatedAtActionResult()
        {
            var task = new TaskDal();
            var result = await _controller.CreateTask(task);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        private void SetTestTasks()
        {
            var user = new UserDal
            {
                Id = 1,
                IsAdmin = false,
                Tasks = new List<TaskDal>()
            };
            var admin = new UserDal
            {
                Id = 2,
                IsAdmin = true,
                Tasks = new List<TaskDal>()
            };

            _testTasks.Add(
                new TaskDal
                {
                    Id = 1,
                    IsDone = false,
                    Owner = user,
                    OwnerId = 1,
                    TaskDescription = "testtask1"
                });
            _testTasks.Add(
                new TaskDal
                {
                    Id = 2,
                    IsDone = true,
                    Owner = user,
                    OwnerId = 1,
                    TaskDescription = "testtask2"
                });
            _testTasks.Add(
                new TaskDal
                {
                    Id = 3,
                    IsDone = false,
                    Owner = admin,
                    OwnerId = 2,
                    TaskDescription = "testtask3"
                });
            _testTasks.Add(
                new TaskDal
                {
                    Id = 4,
                    IsDone = true,
                    Owner = admin,
                    OwnerId = 2,
                    TaskDescription = "testtask4"
                });
        }

        private List<TaskDal> GetTestTasks() => _testTasks;
    }
}
