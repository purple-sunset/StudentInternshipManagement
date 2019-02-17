using System;
using System.ComponentModel.DataAnnotations;

namespace StudentInternshipManagement.Models.Entities
{
    public class News : BaseEntity
    {
        public override int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Time { get; set; }
    }
}