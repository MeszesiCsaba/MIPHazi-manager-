using System;
using System.Collections.Generic;
using System.Text;

namespace MIPHazi
{
    public class Security : Encoding
    {
        public string Encode(string text)
        {
            int shift = 3;
            StringBuilder encoded = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                char character = text[i];
                encoded.Append((char)(character + shift));
            }

            return encoded.ToString();
        }

        public static string HashPassword(string password)
        {
            Security security = new Security();

            return security.Encode(password);
        }

        public static bool VerifyPassword(string rawPassword, string encodedPassword)
        {
            Security security = new Security();
           
            return security.Encode(rawPassword) == encodedPassword;
        }
    }
}