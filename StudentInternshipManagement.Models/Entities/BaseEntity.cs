using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentInternshipManagement.Models.Entities
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        public virtual bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}