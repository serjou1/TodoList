using System.Data.Entity;
using TodoList.DAL.Models;

namespace TodoList.DAL
{
    public class TodoListContext : DbContext
    {
        public TodoListContext() : base("DefaultConnection") { }

        public DbSet<UserDal> Users { get; set; }
        public DbSet<TaskDal> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDal>()
                .HasMany(_ => _.Tasks)
                .WithRequired(_ => _.Owner);
        }
    }
}
