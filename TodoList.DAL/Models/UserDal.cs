using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoList.DAL.Models
{
    public class UserDal
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<TaskDal> Tasks { get; set; }
    }
}
