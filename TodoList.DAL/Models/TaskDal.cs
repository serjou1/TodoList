using System.ComponentModel.DataAnnotations;

namespace TodoList.DAL.Models
{
    public class TaskDal
    {
        [Key]
        public int Id { get; set; }
        public string TaskDescription { get; set; }
        public bool IsDone { get; set; }
        public UserDal Owner { get; set; }
    }
}