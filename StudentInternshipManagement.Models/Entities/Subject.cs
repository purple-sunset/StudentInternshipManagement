using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

namespace StudentInternshipManagement.Models.Entities
{
    public class Subject : BaseEntity
    {
        //[Key]
        [Required]
        [DisplayName("Mã môn học")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SubjectCode { get; set; }

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