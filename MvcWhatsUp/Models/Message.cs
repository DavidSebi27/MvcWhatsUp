namespace MvcWhatsUp.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public int SenderUserId { get; set; }
        public int ReceiverUserId { get; set; }
        public string MessageText { get; set; }
        public DateTime SendAt { get; set; }

        public Message()
        {
        }

        public Message(int id, int senderUserId, int receiverUserId, string messageText, DateTime sendAt)
        {
            MessageId = id;
            SenderUserId = senderUserId;
            ReceiverUserId = receiverUserId;
            MessageText = messageText;
            SendAt = sendAt;
        }
    }

    public class SimpleMessage
    {
        public string Name { get; set; }
        public string MessageText { get; set; }

        public SimpleMessage()
        {
        }

        public SimpleMessage(string name, string messageText)
        {
            Name = name;
            MessageText = messageText;
        }
    }
}