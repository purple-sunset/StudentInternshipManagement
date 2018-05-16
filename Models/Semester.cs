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
    public class Semester
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Mã học kỳ")]
        public int SemesterId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Ngày bắt đầu")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Ngày kết thúc")]
        public DateTime EndDate { get; set; }
    }
}
