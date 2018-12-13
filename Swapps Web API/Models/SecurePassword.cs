using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SWApps2.Converters
{
    public class SecurePassword
    {

        public static byte[] GetSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetNonZeroBytes(salt = new byte[16]);
            return salt;
        }

        public static string Hash(string password, byte[] salt)
        {
            var hashedPassword = new Rfc2898DeriveBytes(password, salt, 10000);
            string hash = Convert.ToBase64String(hashedPassword.GetBytes(20));
            return hash;
        }

        public static bool ConfirmPassword(string passwordHash, string passwordSalt, string password)
        {
            string enteredHashedPassword = Hash(password, Convert.FromBase64String(passwordSalt));
            return passwordHash.Equals(enteredHashedPassword);
        }
    }
}