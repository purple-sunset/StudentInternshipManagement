using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;
using StudentInternshipManagement.Models.Constants;

namespace StudentInternshipManagement.Models.Entities
{
    public class Message : BaseEntity
    {
        public override int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        [MaxLength(50)]
        public string File { get; set; }

        [Range(0, 3)]
        public MessageStatus Status { get; set; }

        [ForeignKey("Sender")]
        public string SenderId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ApplicationUser Sender { get; set; }

        [ForeignKey("Receiver")]
        public string ReceiverId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ApplicationUser Receiver { get; set; }
    }
}