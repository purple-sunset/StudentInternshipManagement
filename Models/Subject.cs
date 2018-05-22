using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Models
{
    public class Subject
    {
        [Key]
        [DisplayName("Mã môn học")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SubjectId { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Tên môn học")]
        public string SubjectName { get; set; }

        [DisplayName("Khoa/Viện")]
        [UIHint("DepartmentTemplate")]
        public int DepartmentId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual Department Department { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<TrainingMajor> TrainingMajors { get; set; }
    }
}
