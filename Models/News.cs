using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class News
    {
        public int NewsId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }
}
