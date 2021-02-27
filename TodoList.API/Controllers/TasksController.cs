using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.DAL;
using TodoList.DAL.Interfaces;
using TodoList.DAL.Models;

namespace TodoList.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TasksController : ControllerBase
    {
        //private readonly ILogger _logger;

        private readonly IRepository<TaskDal> _taskRepository;

        public TasksController(
            IRepository<TaskDal> taskRepository)
        {
            //_logger = logger;

            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<TaskDal>> GetTasksAsync()
            => await _taskRepository.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskAsync(int id)
        {
            var task = await _taskRepository.GetAsync(id);

            if (task is null)
                return NotFound($"Task with id {id} was not found");

            return Ok(task);
        }

        [HttpGet("Done")]
        public async Task<IEnumerable<TaskDal>> GetDoneTasksAsync()
            => await _taskRepository.FindAsync(t => t.IsDone);

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskDal task)
        {
            // todo add owner 
            await _taskRepository.CreateAsync(task);

            return CreatedAtAction(
                nameof(GetTaskAsync),
                new { id = task.Id },
                task);
        }
    }
}
