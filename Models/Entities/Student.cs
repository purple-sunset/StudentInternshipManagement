using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

namespace Models.Entities
{
    public class Student:BaseEntity
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Index(IsUnique = true)]
        [DisplayName("Mã sinh viên")]
        public string StudentCode { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Tên sinh viên")]
        public string StudentName { get; set; }

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

        [Required]
        [Range(0,4)]
        [DisplayName("Điểm CPA")]
        public float Cpa { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Chương trình đào tạo")]
        public string Program { get; set; }

        [DisplayName("Lớp học")]
        [UIHint("StudentClassTemplate")]
        public int ClassId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual StudentClass Class { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<LearningClassStudent> LearningClassStudents { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Message> Messages { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        [InverseProperty("Members")]
        public virtual ICollection<Group> Groups { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
