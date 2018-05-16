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

        public string SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        public int SemesterId { get; set; }

        public virtual Semester Semester { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
