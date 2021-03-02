using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoList.DAL;
using TodoList.DAL.Interfaces;
using TodoList.DAL.Models;
using TodoList.DAL.Repositories.Exceptions;

namespace TodoList.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IRepository<TaskDal> _taskRepository;
        private readonly IRepository<UserDal> _userRepository;

        public TasksController(
            IRepository<TaskDal> taskRepository,
            IRepository<UserDal> userRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
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

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskDal task)
        {
            var userIdString = User?.FindFirstValue(ClaimTypes.SerialNumber);
            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized();

            if (!int.TryParse(userIdString, out var userId))
                return BadRequest();

            var user = await _userRepository.GetAsync(userId);

            task.Owner = user;
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

            if (!User.IsInRole("Admin"))
            {
                var userIdString = User.FindFirstValue(ClaimTypes.SerialNumber);
                if (task.OwnerId.ToString() != userIdString)
                    return Unauthorized($"Task with id {id} does not belong to user with id {userIdString}");
            }

            if (task is null)
                return NotFound($"Task with id {id} was not found");

            patchTask.ApplyTo(task);
            await _taskRepository.UpdateAsync(task);

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {

            if (!User.IsInRole("Admin"))
            {
                var task = await _taskRepository.GetAsync(id);
                var userIdString = User.FindFirstValue(ClaimTypes.SerialNumber);
                if (task.OwnerId.ToString() != userIdString)
                    return Unauthorized($"Task with id {id} does not belong to user with id {userIdString}");
            }

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
