using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptedMsg
{
    class TextCoder
    {
        public static byte[] Encode(string str)
        {
            byte[] temp = new byte[str.Length * 2];

            for (int i = 0; i < str.Length; i++)
            {
                temp[i * 2] = (byte)(str[i] >> 8);
                temp[i * 2 + 1] = (byte)str[i];
            }

            return temp;
        }

        public static string Decode(byte[] buf)
        {
            char[] str = new char[buf.Length / 2];

            for (int i = 0; i < buf.Length / 2; i++)
            {
                str[i] = (char)((int)(buf[i * 2] << 8) + (int)(buf[i * 2 + 1]));
            }

            return new string(str);
        }

        public static string Decode(byte[] buf, int end)
        {
            char[] str = new char[end / 2];

            for (int i = 0; i < end / 2; i++)
            {
                str[i] = (char)((int)(buf[i * 2] << 8) + (int)(buf[i * 2 + 1]));
            }

            return new string(str);
        }
    }
}
