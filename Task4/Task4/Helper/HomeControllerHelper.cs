using System;
using System.Collections.Generic;
using System.Linq;
using Task4.Models;

namespace Task4.Helper
{
    internal static class HomeControllerHelper
    {
        internal static List<Message> GetMessagesFromDb(MessageContext db)
        {
            var messages = db.Messages.ToList();
            return messages;
        }

        internal static Message GetMessage(IEnumerable<Message> messages, int messageType)
        {
            var message = messages.Where(m => m.MessageType == messageType).Select(m => m).FirstOrDefault();
            return message;
        }

        internal static int GetMessageCount(MessageContext db, int messageType)
        {
            var messages = db.Messages.ToList().Where(m => m.MessageType == messageType).Select(m => m);
            return messages.FirstOrDefault().Count;
        }

        internal static Message GetMessageFromDb(MessageContext db, int messageType)
        {
            var messages = db.Messages.ToList().Where(m => m.MessageType == messageType).Select(m => m);
            return messages.FirstOrDefault();
        }

        internal static Message InitializeMessage(Message message, string userName, string text)
        {
            message.SenderName = userName;
            message.SendDate = DateTime.Now.ToString("hh:mm:ss");
            message.Text = text;
            message.Count++;
            return message;
        }

        private static void UpdateMessage(IEnumerable<Message>messages, int messageType, string userName, string text)
        {
            var message = GetMessage(messages, messageType);
            InitializeMessage(message, userName, text);
        }

        internal static void UpdateMessages(IEnumerable<Message> messages, int messageType, string userName, string text)
        {
            UpdateMessage(messages, messageType, userName, text);
            UpdateMessage(messages, MessageType.LAST_MESSAGE, userName, text);    
        }
    }
}
