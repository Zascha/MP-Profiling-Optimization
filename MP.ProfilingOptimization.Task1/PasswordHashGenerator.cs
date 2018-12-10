using System;
using System.Security.Cryptography;
using System.Text;

namespace MP.ProfilingOptimization.Task1
{
    public class PasswordHashGenerator
    {
        public string GeneratePasswordHashUsingSalt_NotOptimized(string passwordText, byte[] salt)
        {
            var iterate = 10000;
            var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            var passwordHash = Convert.ToBase64String(hashBytes);

            return passwordHash;
        }

        public string GeneratePasswordHashUsingSalt_Optimized(string passwordText, byte[] salt)
        {
            byte[] hash = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(passwordText), salt, 10000).GetBytes(20);
            byte[] hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
