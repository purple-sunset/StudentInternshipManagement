using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace StudentInternshipManagement.Models.Entities
{
    public class LearningClass : BaseEntity
    {
        [Key]
        [DisplayName("Mã lớp")]
        public override int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Tên lớp")]
        public string ClassName { get; set; }

        [DisplayName("Môn học")]
        [UIHint("SubjectTemplate")]
        public int SubjectId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual Subject Subject { get; set; }

        [DisplayName("Học kỳ")]
        [UIHint("SemesterTemplate")]
        public int SemesterId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual Semester Semester { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        [DisplayName("Danh sách sinh viên")]
        public virtual ICollection<LearningClassStudent> LearningClassStudents { get; set; }
    }
}