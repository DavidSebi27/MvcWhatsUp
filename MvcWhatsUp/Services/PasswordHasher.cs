using System;
using System.Security.Cryptography;
using System.Text;


namespace MvcWhatsUp.Services

{
    public class PasswordHasher
    {

        // Generate random salt
        public static byte[] GenerateSalt(int size = 16)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] salt = new byte[size];
                rng.GetBytes(salt);
                return salt;
            }
        }

        public static string HashPassword(string password, byte[] salt)
        {
            using (SHA256 sha256 = SHA256.Create()) //obj is created to performn hashing
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password); // converts password string into a byte array using UTF-8 encoding
                byte[] combinedBytes = new byte[salt.Length + passwordBytes.Length]; // creates a new byte array, containing both salt and passwordBytes

                Buffer.BlockCopy(salt, 0, combinedBytes, 0, salt.Length);  //copies all bytes from salt to combinedBytes salt: source array, 0: starting index (from which index to copy)
                                                                           //combinedBytes: destination array, 0: starting index in the destination, salt.lenght: the num of bytes to copy
                Buffer.BlockCopy(passwordBytes, 0, combinedBytes, salt.Length, passwordBytes.Length); //same thing as above

                byte[] hashBytes = sha256.ComputeHash(combinedBytes); //computes the sha-256 hash of the combined array, resulting in a hashed byte array

                // fun part:
                return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hashBytes); //okay so this was really fun. i save both the salt and the hashed salted (lol) password in one place, nothing can go wrong... right?
            }
        }

        // lets also verify the password while im at it, why not
        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            string[] parts = storedHash.Split(":"); //splitting stored hash
            if (parts.Length != 2) { return false; }

            byte[] salt = Convert.FromBase64String(parts[0]);
            string expectedHash = parts[1];

            string newHash = HashPassword(enteredPassword, salt).Split(":")[1]; //hash entered password with the salt -- fun

            return newHash == expectedHash; //verify, also blow up or idk
        }
    }
}
