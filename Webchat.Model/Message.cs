using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webchat.Model
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }
        public int Sender { get; set; }
        public int ChatId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime? SendTime { get; set; }

    }
}
