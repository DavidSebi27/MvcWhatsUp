using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MvcWhatsUp.Services
{
    public interface IEncryptionService
    {
        string Encrypt(string plainText);
        string Decrypt(string encryptedText);
    }

    public class EncryptionService : IEncryptionService
    {
        private readonly string _keyPath;
        private readonly string _keyPassword;

        public EncryptionService(string keyPath, string keyPassword)
        {
            _keyPath = keyPath;
            _keyPassword = keyPassword;
        }

        public string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return plainText;

            using (var cert = new X509Certificate2(_keyPath, _keyPassword))
            using (var rsa = cert.GetRSAPublicKey())
            {
                byte[] data = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedData = rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
                return Convert.ToBase64String(encryptedData);
            }
        }

        public string Decrypt(string encryptedText)
        {
            if (string.IsNullOrEmpty(encryptedText)) return encryptedText;

            using (var cert = new X509Certificate2(_keyPath, _keyPassword))
            using (var rsa = cert.GetRSAPrivateKey())
            {
                byte[] data = Convert.FromBase64String(encryptedText);
                byte[] decryptedData = rsa.Decrypt(data, RSAEncryptionPadding.OaepSHA256);
                return Encoding.UTF8.GetString(decryptedData);
            }
        }
    }
}