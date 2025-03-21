using Microsoft.Data.SqlClient;
using MvcWhatsUp.Models;
using MvcWhatsUp.Repositories;

public class DbChatsRepository : IChatsRepository
{
    private readonly string? _connectionString;

    public DbChatsRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("WhatsUpDatabase");
    }

    private Message ReadMessage(SqlDataReader reader)
    {
        int id = (int)reader["MessageId"];
        int senderUserId = (int)reader["SenderUserId"];
        int receiverUserId = (int)reader["ReceiverUserId"];
        string messageText = (string)reader["Message"];
        DateTime sendAt = (DateTime)reader["SendAt"];

        return new Message(id, senderUserId, receiverUserId, messageText, sendAt);
    }

    public void AddMessage(Message message)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "INSERT INTO Messages (SenderUserId, ReceiverUserId, Message, SendAt) " +
                           "VALUES (@SenderUserId, @ReceiverUserId, @Message, @SendAt);" +
                           "SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@SenderUserId", message.SenderUserId);
            cmd.Parameters.AddWithValue("@ReceiverUserId", message.ReceiverUserId);
            cmd.Parameters.AddWithValue("@Message", message.MessageText);
            cmd.Parameters.AddWithValue("@SendAt", message.SendAt);

            cmd.Connection.Open();
            message.MessageId = Convert.ToInt32(cmd.ExecuteScalar());
        }
    }

    public List<Message> GetMessages(int userId1, int userId2)
    {
        List<Message> messages = new List<Message>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT MessageId, SenderUserId, ReceiverUserId, Message, SendAt FROM Messages " +
                           "WHERE (SenderUserId = @UserId1 AND ReceiverUserId = @UserId2) OR " +
                           "(SenderUserId = @UserId2 AND ReceiverUserId = @UserId1) " +
                           "ORDER BY SendAt ASC;";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserId1", userId1);
            cmd.Parameters.AddWithValue("@UserId2", userId2);

            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Message message = ReadMessage(reader);
                messages.Add(message);
            }
            reader.Close();
        }

        return messages;
    }
}