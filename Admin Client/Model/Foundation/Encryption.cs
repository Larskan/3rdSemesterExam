using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Model.Foundation
{
    public class Encryption
    {
        /// <summary>
        /// Encrypt password via SHA256 encoding
        /// </summary>
        /// <param name="password">The password</param>
        /// <param name="salt">The amount of salt that gets added</param>
        /// <returns>Encrypted password</returns>
        public static string Encrypt_Password(string password, int salt)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {

                //convert the given password and the computed salt to SHA256, byte for byte
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password + salt));

                //afterwards, inorder to construct the hash we pass off the bytes to a stringbuilder to get it in one line
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                //return the encrypted password
                return sb.ToString();
            }
        }
        /// <summary>
        /// Calculate the amount of salt the password is going to get
        /// </summary>
        /// <param name="password">The password</param>
        /// <returns>The amount of salt</returns>
        public static int Salt_Password(string password)
        {
            //Inorder to salt the password, we take each char in the given password, add it together in a sum
            
            int charAsDecimal = 0;
            foreach (char character in password)
            {
                charAsDecimal = Convert.ToInt32(character);
            }
            //after computing the sum we take the passwords length and calculate that passwords sum, such as a password with length 3 becomes 3+2+1 = 6.
            int total = charAsDecimal + ((password.Length * password.Length / 2) + (password.Length / 2));
            //lastly take the computed sum and return it to be used as a salt.
            return total;
        }
    }
}
