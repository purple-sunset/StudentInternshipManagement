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
    public class Teacher:BaseEntity
    {
        //[Key]
        [Required]
        [DisplayName("Mã giảng viên")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        [Required]
        [MaxLength(50)]
        [ScaffoldColumn(false)]
        [DisplayName("Ảnh")]
        [DefaultValue("avatar.png")]
        public string Avatar { get; set; }

        [DisplayName("Khoa/Viện")]
        [UIHint("DepartmentTemplate")]
        public int DepartmentId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual Department Department { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Message> Messages { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Group> Groups { get; set; }
    }
}
