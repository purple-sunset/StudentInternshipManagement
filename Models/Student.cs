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
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Mã sinh viên")]
        public string StudentId { get; set; }

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
        [MaxLength(50)]
        [ScaffoldColumn(false)]
        [DisplayName("Ảnh")]
        [DefaultValue("avatar.png")]
        public string Avatar { get; set; }

        [Required]
        [Range(0,4)]
        [DisplayName("Điểm CPA")]
        public float Cpa { get; set; }

        [DisplayName("Lớp học")]
        [UIHint("StudentClassTemplate")]
        public int ClassId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual StudentClass Class { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<LearningClass> LearningClasses { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Message> Messages { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        [InverseProperty("Members")]
        public virtual ICollection<Group> Groups { get; set; }
    }
}
