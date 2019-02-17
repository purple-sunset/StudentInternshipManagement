using System.ComponentModel.DataAnnotations;

namespace StudentInternshipManagement.Models.Constants
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

    public enum MessageStatus
    {
        [Display(Name = "Nháp")]
        Draft,

        [Display(Name = "Đã gửi")]
        Sent,

        [Display(Name = "Đã đọc")]
        Read
    }
}