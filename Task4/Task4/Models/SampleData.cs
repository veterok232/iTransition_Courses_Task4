using System.Linq;

namespace Task4.Models
{
    public class SampleData
    {
        public static void Initialize(MessageContext context)
        {
            if (!context.Messages.Any())
            {
                context.Messages.AddRange(
                    new Message { MessageType = MessageType.BRO_MESSAGE, SenderName = "No messages" },
                    new Message { MessageType = MessageType.SIS_MESSAGE, SenderName = "No messages" },
                    new Message { MessageType = MessageType.LAST_MESSAGE, SenderName = "No messages" });
            }
            context.SaveChanges();
        }
    }
}
