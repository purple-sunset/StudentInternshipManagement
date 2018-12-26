using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

namespace Models.Entities
{
    public class Teacher : BaseEntity
    {
        //[Key]
        [Required]
        [Index(IsUnique = true)]
        [DisplayName("Mã giảng viên")]
        public string TeacherCode { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Tên giảng viên")]
        public string TeacherName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Ngày sinh")]
        public DateTime BirthDate { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [Required]
        [MaxLength(15)]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }

        [DisplayName("Khoa/Viện")]
        [UIHint("DepartmentTemplate")]
        public int DepartmentId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual Department Department { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Message> Messages { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Group> Groups { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}