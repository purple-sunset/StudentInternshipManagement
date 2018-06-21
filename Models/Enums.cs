using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum InternshipStatus
    {
        [Display(Name = "Chưa xử lý")]
        Registered,
        [Display(Name = "Xử lý thành công")]
        Success,
        [Display(Name = "Xử lý thất bại")]
        Failed,
        [Display(Name = "Hoàn thành")]
        Done
    }
}
