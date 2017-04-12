using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TOTPModel;
using System.Security.Cryptography;

namespace TOTPBL
{
    public class BL
    {
        #region "OTP Methods"
        
        private const int IN_BYTE_SIZE = 8;
        private const int OUT_BYTE_SIZE = 5;
        private static char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567".ToCharArray();

        public System.Data.Linq.Binary generateUserSecretKey()
        {
            RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();

            byte[] key = new byte[10];
            _rng.GetBytes(key);

            return key;
        }

        /// <summary>
        /// Based off of https://github.com/esp0/googleAuthNet/blob/master/Transcoder.cs
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public String Base32(byte[] byteKey)
        {
            int i = 0, index = 0, digit = 0;
            int current_byte, next_byte;
            StringBuilder result = new StringBuilder((byteKey.Length + 7) * IN_BYTE_SIZE / OUT_BYTE_SIZE);

            while (i < byteKey.Length)
            {
                current_byte = (byteKey[i] >= 0) ? byteKey[i] : (byteKey[i] + 256); // Unsign

                /* Is the current digit going to span a byte boundary? */
                if (index > (IN_BYTE_SIZE - OUT_BYTE_SIZE))
                {
                    if ((i + 1) < byteKey.Length)
                        next_byte = (byteKey[i + 1] >= 0) ? byteKey[i + 1] : (byteKey[i + 1] + 256);
                    else
                        next_byte = 0;

                    digit = current_byte & (0xFF >> index);
                    index = (index + OUT_BYTE_SIZE) % IN_BYTE_SIZE;
                    digit <<= index;
                    digit |= next_byte >> (IN_BYTE_SIZE - index);
                    i++;
                }
                else
                {
                    digit = (current_byte >> (IN_BYTE_SIZE - (index + OUT_BYTE_SIZE))) & 0x1F;
                    index = (index + OUT_BYTE_SIZE) % IN_BYTE_SIZE;
                    if (index == 0)
                        i++;
                }
                result.Append(alphabet[digit]);
            }

            return result.ToString();
        }

        public int calculateOneTimePassword(System.Data.Linq.Binary binSecret)
        {
            byte[] secret = binSecret.ToArray();

            // https://tools.ietf.org/html/rfc4226
            Int64 Timestamp = Convert.ToInt64(GetUnixTimestamp() / 30);
            var data = BitConverter.GetBytes(Timestamp).Reverse().ToArray();
            byte[] hmac = new HMACSHA1(secret).ComputeHash(data);
            int offset = hmac.Last() & 0x0F;
            int oneTimePassword = (
                ((hmac[offset + 0] & 0x7f) << 24) |
                ((hmac[offset + 1] & 0xff) << 16) |
                ((hmac[offset + 2] & 0xff) << 8) |
                (hmac[offset + 3] & 0xff)
                    ) % 1000000;

            return oneTimePassword;
        }

        private static Int64 GetUnixTimestamp()
        {
            return Convert.ToInt64(Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds));
        }

        #endregion
        
        #region "Helper Methods"

        private static byte[] GetBytes(String data)
        {
            List<byte> retVal = new List<byte>();

            foreach (char d in data)
            {
                retVal.Add((byte)d);
            }
            

            return retVal.ToArray();
        }

        #endregion

    }
}
