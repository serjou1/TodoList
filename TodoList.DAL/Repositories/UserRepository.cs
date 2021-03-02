using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoList.DAL.Interfaces;
using TodoList.DAL.Models;

namespace TodoList.DAL.Repositories
{
    public class UserRepository : IRepository<UserDal>
    {
        private readonly TodoListContext _context;

        public UserRepository(TodoListContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(UserDal user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = new UserDal { Id = id };

            _context.Users.Attach(user);
            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDal>> FindAsync(Expression<Func<UserDal, bool>> predicate)
        {
            return await _context.Users.Where(predicate).ToArrayAsync();
        }

        public async Task<IEnumerable<UserDal>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<UserDal> GetAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task UpdateAsync(UserDal user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
