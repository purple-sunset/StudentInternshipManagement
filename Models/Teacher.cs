using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TeacherId { get; set; }

        [Required]
        [MaxLength(50)]
        public string TeacherName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        [MaxLength(15)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(50)]
        public string Avatar { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<Group> Groups { get; set; }
    }
}
