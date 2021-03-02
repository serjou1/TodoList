using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.DAL.Models
{
    public class TaskDal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TaskDescription { get; set; }
        public bool IsDone { get; set; }

        public int OwnerId { get; set; }
        public UserDal Owner { get; set; }
    }
}