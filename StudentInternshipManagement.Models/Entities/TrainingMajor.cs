using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace StudentInternshipManagement.Models.Entities
{
    public class TrainingMajor : BaseEntity
    {
        [DisplayName("Mã định hướng")]
        public override int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Tên định hướng")]
        public string TrainingMajorName { get; set; }

        [DisplayName("Môn học")]
        [UIHint("SubjectTemplate")]
        public int SubjectId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual Subject Subject { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        [DisplayName("Danh sách công ty")]
        public virtual ICollection<CompanyTrainingMajor> CompanyTrainingMajors { get; set; }
    }
}