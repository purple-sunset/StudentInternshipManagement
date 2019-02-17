using System.ComponentModel.DataAnnotations;

namespace StudentInternshipManagement.Models.Entities
{
    public class EmailTemplate : BaseEntity
    {
        public override int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Body { get; set; }
    }
}