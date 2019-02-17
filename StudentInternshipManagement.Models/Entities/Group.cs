using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

namespace StudentInternshipManagement.Models.Entities
{
    public class Group : BaseEntity
    {
        [DisplayName("Mã nhóm")]
        public override int Id { get; set; }

        [DisplayName("Tên nhóm")]
        public string GroupName { get; set; }

        [DisplayName("Công ty thực tập")]
        public int CompanyId { get; set; }

        [DisplayName("Định hướng")]
        public int TrainingMajorId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual CompanyTrainingMajor Major { get; set; }

        [DisplayName("Lớp học")]
        public int ClassId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual LearningClass Class { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Student> Members { get; set; }

        [DisplayName("Nhóm trưởng")]
        [ForeignKey("Leader")]
        public int LeaderId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual Student Leader { get; set; }

        [DisplayName("Giảng viên hướng dẫn")]
        public int TeacherId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual Teacher Teacher { get; set; }
    }
}