using MvcWhatsUp.Models;
using Microsoft.Data.SqlClient;
using MvcWhatsUp.Services;

namespace MvcWhatsUp.Repositories
{
    public class DbUsersRepository : IUsersRepository
    {
        private readonly string? _connectionString;

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
            string password = (string)reader["AuthData"];

            return new User(id, name, mobileNumber, emailAddress, password);
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT UserId, UserName, MobileNumber, EmailAddress, AuthData FROM Users;";
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
            User user = new User();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"SELECT UserId, UserName, MobileNumber, EmailAddress, AuthData FROM Users WHERE Users.UserId = {userId};";
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                     user = ReadUser(reader);
                }
                reader.Close();
            }
            return user;


        }

        public void Add(User user)
        {

            //generate salt and hash password
            byte[] salt = PasswordHasher.GenerateSalt();
            string hashedPassword = PasswordHasher.HashPassword(user.Password, salt);


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Users (UserName, MobileNumber, EmailAddress, AuthData)" +
                                "VALUES (@Name, @Number, @Email, @Password);" +
                                "SELECT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@Name", user.UserName);
                cmd.Parameters.AddWithValue("@Number", user.MobileNumber);
                cmd.Parameters.AddWithValue("@Email", user.EmailAddress);
                cmd.Parameters.AddWithValue("@Password", hashedPassword);

                cmd.Connection.Open();
                user.UserId = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Update(User user)
        {

            //generate salt and hash password
            byte[] salt = PasswordHasher.GenerateSalt();
            string hashedPassword = PasswordHasher.HashPassword(user.Password, salt);


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Users SET UserName = @Name, MobileNumber = @Number, EmailAddress = @Email, AuthData = @Password " +
                    "WHERE Users.UserId = @Id;";
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@Id", user.UserId);
                cmd.Parameters.AddWithValue("@Name", user.UserName);
                cmd.Parameters.AddWithValue("@Number", user.MobileNumber);
                cmd.Parameters.AddWithValue("@Email", user.EmailAddress);
                cmd.Parameters.AddWithValue("@Password", hashedPassword);

                cmd.Connection.Open();

                int nrOfRowsAffected = cmd.ExecuteNonQuery();
                if (nrOfRowsAffected == 0)
                {
                    throw new Exception("No records updated!");
                }
            }
        }

        public void Delete(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Users WHERE UserId = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", user.UserId);

                cmd.Connection.Open();

                int rowsAffected = cmd.ExecuteNonQuery();  // Actually executes the DELETE statement

                if (rowsAffected == 0)
                {
                    throw new Exception("No records deleted! User might not exist.");
                }
            }
        }
    }
}
