using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SWApps2.Converters
{
    public class SecurePassword
    {
        public static byte[] Hash(string password, byte[] salt)
        {
            return Hash(Encoding.UTF8.GetBytes(password), salt);
        }

        private static byte[] Hash(byte[] passwordBytes, byte[] salt)
        {
            byte[] saltedPassword = passwordBytes.Concat(salt).ToArray();
            return new SHA256Managed().ComputeHash(saltedPassword);
        }

        public static bool ConfirmPassword(byte[] passwordHash, byte[] passwordSalt, string password)
        {
            byte[] enteredHashedPassword = Hash(password, passwordSalt);
            return passwordHash.SequenceEqual(enteredHashedPassword);
        }
    }
}