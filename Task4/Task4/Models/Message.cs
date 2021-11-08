using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task4.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string SenderName { get; set; }
        
        public string SendDate { get; set; }
        
        public int MessageType { get; set; }
        public string Text { get; set; }

        public int Count { get; set; }
     
        public override string ToString()
        {
            return $"Sent by {SenderName} at {SendDate}";
        }
    }
}
