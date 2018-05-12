using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string StudentId { get; set; }

        [Required]
        [MaxLength(50)]
        public string StudentName { get; set; }

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

        [Required]
        [Range(0,4)]
        public float Cpa { get; set; }

        public int ClassId { get; set; }

        public StudentClass Class { get; set; }

        public ICollection<LearningClass> LearningClasses { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<Group> Groups { get; set; }
    }
}
