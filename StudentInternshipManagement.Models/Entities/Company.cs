using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace StudentInternshipManagement.Models.Entities
{
    public class Company : BaseEntity
    {
        [DisplayName("Mã công ty")]
        public override int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Tên công ty")]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Mô tả")]
        public string CompanyDescription { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(15)]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        [DisplayName("Danh sách định hướng")]
        public virtual ICollection<CompanyTrainingMajor> CompanyTrainingMajors { get; set; }
    }
}