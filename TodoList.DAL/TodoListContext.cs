using Microsoft.EntityFrameworkCore;
using TodoList.DAL.Models;

namespace TodoList.DAL
{
    public class TodoListContext : DbContext
    {
        public TodoListContext(DbContextOptions<TodoListContext> options)
            : base(options) { }

        public DbSet<UserDal> Users { get; set; }
        public DbSet<TaskDal> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskDal>()
                .HasOne(x => x.Owner)
                .WithMany();

            modelBuilder.Entity<UserDal>()
                .HasData(
                new UserDal { UserName = "TestUser", IsAdmin = false, Id = 1 },
                new UserDal { UserName = "TestAdmin", IsAdmin = true, Id = 2 });

            base.OnModelCreating(modelBuilder);
        }
    }
}
