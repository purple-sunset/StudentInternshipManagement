using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Notification
    {
        public int NotificationId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Content { get; set; }

        [Required]
        [MaxLength(50)]
        public string Url { get; set; }
        
        public DateTime Time { get; set; }
    }
}
