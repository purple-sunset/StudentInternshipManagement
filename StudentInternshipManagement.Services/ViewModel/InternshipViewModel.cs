using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using StudentInternshipManagement.Models.Constants;

namespace StudentInternshipManagement.Services.ViewModel
{
    public class InternshipViewModel
    {
        [DisplayName("Mã thực tập")]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Ngày đăng ký")]
        public DateTime RegistrationDate { get; set; }

        [DisplayName("Trạng thái")]
        public InternshipStatus Status { get; set; }

        [DisplayName("Sinh viên")]
        public string Student { get; set; }

        [DisplayName("Lớp học")]
        public string Class { get; set; }

        [DisplayName("Học kỳ")]
        public string Semester { get; set; }

        [DisplayName("Công ty")]
        public string Company { get; set; }

        [DisplayName("Định hướng")]
        public string TrainingMajor { get; set; }

        [DisplayName("Điểm giữa kỳ")]
        public float? MidTermPoint { get; set; }

        [DisplayName("Điểm cuối kỳ")]
        public float? EndTermPoint { get; set; }

        [DisplayName("Điểm tổng kết")]
        public float? TotalPoint { get; set; }

        [DisplayName("Nhóm")]
        public string Group { get; set; }

        [DisplayName("Giảng viên hướng dẫn")]
        public string Teacher { get; set; }
    }
}