using Microsoft.AspNetCore.JsonPatch;
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
using TodoList.DAL.Repositories.Exceptions;

namespace TodoList.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IRepository<TaskDal> _taskRepository;

        public TasksController(
            IRepository<TaskDal> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasksAsync([FromQuery] Filter filter)
        {
            var list = await _taskRepository.FindAsync(t => t.IsDone == (filter.IsDone ?? t.IsDone));
            return Ok(list);
        }

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

        [HttpPatch("{id}")]
        public async Task<IActionResult> EditTask(int id, [FromBody] JsonPatchDocument<TaskDal> patchTask)
        {
            var task = await _taskRepository.GetAsync(id);

            if (task is null)
                return NotFound($"Task with id {id} was not found");

            patchTask.ApplyTo(task);
            await _taskRepository.UpdateAsync(task);

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                await _taskRepository.DeleteAsync(id);
                return Ok($"Task with id {id} was deleted");
            }
            catch (EntryDoesNotExistsException)
            {
                return NotFound($"Task with id {id} was not found");
            }
        }
    }
}
