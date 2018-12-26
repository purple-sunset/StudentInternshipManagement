using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace Models.Entities
{
    public class StudentClass : BaseEntity
    {
        [Key]
        [DisplayName("Mã lớp")]
        public override int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Tên lớp")]
        public string ClassName { get; set; }

        [DisplayName("Khoa/Viện")]
        [UIHint("DepartmentTemplate")]
        public int DepartmentId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual Department Department { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        [DisplayName("Danh sách sinh viên")]
        public virtual ICollection<Student> Students { get; set; }
    }
}