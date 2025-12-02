using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD41HN_HFT_2022231.Models
{
    public class ChatMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Sender { get; set; }    // Ki küldte? (pl. "Orvos" vagy "asd")
        public string Message { get; set; }   // Mit?
        public string RoomName { get; set; }  // Melyik szobába? (pl. "asd")
        public DateTime Timestamp { get; set; } // Mikor?
    }
}