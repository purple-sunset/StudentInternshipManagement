using System;
using System.ComponentModel.DataAnnotations;

namespace StudentInternshipManagement.Models.Entities
{
    public class Notification : BaseEntity
    {
        public override int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Content { get; set; }

        [Required]
        [MaxLength(50)]
        public string Url { get; set; }

        public DateTime Time { get; set; }
    }
}