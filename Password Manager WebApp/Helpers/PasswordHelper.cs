using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Password_Manager_WebApp.Helpers
{
    public class PasswordHelper
    {
        public string EncryptPassword(string password)
        {
            byte[] passwordByte;

            for (int i = 0; i <= 2; i++)
            {
                passwordByte = new byte[password.Length];
                passwordByte = System.Text.Encoding.UTF8.GetBytes(password);

                password = Convert.ToBase64String(passwordByte);                
            }

            return password;
        }

        public string DecryptPassword(string password)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            Decoder utf8Decode = encoder.GetDecoder();

            byte[] passwordByte, todecodeByte;
            int charCount;
            char[] decodedChar;

            for (int i = 0; i <= 2; i++)
            {
                passwordByte = Encoding.ASCII.GetBytes(password);
                password = Encoding.ASCII.GetString(passwordByte);

                todecodeByte = Convert.FromBase64String(password);
                charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                password = new String(decodedChar);
            }

            return password;
        }
    }
}