using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentInternshipManagement.Models.Entities
{
    public class EmailHistory : BaseEntity
    {
        public override int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Body { get; set; }

        [Required]
        [MaxLength(1000)]
        public string To { get; set; }

        [Required]
        [MaxLength(1000)]
        // ReSharper disable once InconsistentNaming
        public string CC { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Attachments { get; set; }

        [DisplayName("Đã gửi")]
        public override bool IsDeleted { get; set; }
    }
}