using MvcWhatsUp.Models;
using Microsoft.Data.SqlClient;

namespace MvcWhatsUp.Repositories
{
    public class DbUsersRepository : IUsersRepository
    {
        private readonly string? _connectionString;
        private List<User> users = new List<User>();

        public DbUsersRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("WhatsUpDatabase");
        }

        private User ReadUser(SqlDataReader reader)
        {
            int id = (int)reader["UserId"];
            string name = (string)reader["UserName"];
            string mobileNumber = (string)reader["MobileNumber"];
            string emailAddress = (string)reader["EmailAddress"];
            string password = (string)reader["Password"];

            return new User(id, name, mobileNumber, emailAddress, password);
        }

        public List<User> GetAll()
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT UserId, UserName, MobileNumber, EmailAddress FROM Users;";
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    User user = ReadUser(reader);
                    users.Add(user);
                }
                reader.Close();
            }
            return users;
        }

        public User? GetById(int userId)
        {
            return users.FirstOrDefault(x => x.UserId == userId);
        }

        public void Add(User user)
        {
            /*if (users.Any(u => u.UserId == user.UserId))
            {
                throw new InvalidOperationException("User with the same ID already exists. Chaos Control failed!");
            }
            int newId = users.Count > 0 ? users.Max(u => u.UserId) + 1 : 1; // This is where you specify the userid - Finds highest user id, if list is empty, default is 1
            user.UserId = newId;
            users.Add(user);*/
        }

        public void Update(User user)
        {
            /*User existingUser = users.FirstOrDefault(u => u.UserId == user.UserId);
            if (existingUser == null)
            {
                throw new InvalidOperationException("Agent not found. Perhaps they vanished into the Chaos Void.");
            }
            existingUser.UserName = user.UserName;
            existingUser.MobileNumber = user.MobileNumber;
            existingUser.EmailAddress = user.EmailAddress;
            existingUser.Password = user.Password;*/
        }

        public void Delete(User user)
        {
            /*if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Agent object cannot be null.");
            }

            User existingUser = users.FirstOrDefault(u => u.UserId == user.UserId);
            if (existingUser != null)
            {
                users.Remove(existingUser);
            }
            else
            {
                throw new InvalidOperationException("Agent not found. They’ve gone rogue and disappeared into the shadows.");
            }*/
        }
    }
}
