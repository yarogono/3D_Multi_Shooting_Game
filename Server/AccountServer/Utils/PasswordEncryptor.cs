using System.Text;
using System.Security.Cryptography;

namespace AccountServer.Utils
{
    public class PasswordEncryptor
    {
        private SHA256Managed sha256Managed = new SHA256Managed();

        public string Encrypt(string password)
        {
            byte[] encryptBytes = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(password));

            //base64
            String encryptString = Convert.ToBase64String(encryptBytes);

            return encryptString;
        }

        public bool IsmatchPassword(string inputPassword, string matchPassword)
        {
            string base64AttemptedHash = Encrypt(inputPassword);

            return base64AttemptedHash == matchPassword;
        }
    }
}
