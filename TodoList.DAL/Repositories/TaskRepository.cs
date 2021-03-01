using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TodoList.DAL.Interfaces;
using TodoList.DAL.Models;
using TodoList.DAL.Repositories.Exceptions;

namespace TodoList.DAL.Repositories
{
    public class TaskRepository : IRepository<TaskDal>
    {
        private readonly TodoListContext _context;

        public TaskRepository(TodoListContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(TaskDal task)
        {
            _context.Tasks.Add(task);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                // todo handle this
                //throw new
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var task = new TaskDal { Id = id };

            _context.Tasks.Attach(task);
            _context.Tasks.Remove(task);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (e.Entries.Count == 1 && e.Entries.First().Entity is TaskDal && (e.Entries.First().Entity as TaskDal).Id == id)
                    throw new EntryDoesNotExistsException();

                throw new NotSupportedException(
                    "Don't know how to handle concurrency conflicts",
                    e);
            }
        }

        public async Task<IEnumerable<TaskDal>> FindAsync(Expression<Func<TaskDal, bool>> predicate)
        {
            return await _context.Tasks.Where(predicate).ToArrayAsync();
        }

        public async Task<IEnumerable<TaskDal>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskDal> GetAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);// ?? new 

            // todo if user null

        }

        public async Task UpdateAsync(TaskDal task)
        {
            _context.Entry(task).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                // todo handle
                throw;
            }
        }
    }
}
