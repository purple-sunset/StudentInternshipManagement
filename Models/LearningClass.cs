using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LearningClass
    {
        [Key]
        public int ClassId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ClassName { get; set; }

        public int SubjectId { get; set; }

        public Subject Subject { get; set; }

        public int SemesterId { get; set; }

        public Semester Semester { get; set; }
    }
}
