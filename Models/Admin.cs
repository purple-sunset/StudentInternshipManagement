using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Mã quản lý")]
        public string AdminId { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Tên quản lý")]
        public string AdminName { get; set; }

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

        public virtual Department Department { get; set; }
    }
}
