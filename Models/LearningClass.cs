using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

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

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual Subject Subject { get; set; }

        public int SemesterId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual Semester Semester { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Student> Students { get; set; }
    }
}
