using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TodoList.DAL.Models
{
    public class UserDal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<TaskDal> Tasks { get; set; }
    }
}
