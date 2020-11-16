using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LabProgram.Helper
{

    public static class Cls_Global
    {
        public static string username = "";
        public static string pwd = "12345";
        public static bool IsAmin = false;
        public static int user_id = 0;
        public static int MeterSerialNumber { get; set; }
        public static DateTime ConvertIntToDate(uint date)
        {
            int LastChargeDateFromCard = (int)Convert.ToUInt32(date);
            DateTime LastChargeDateOnCard;
            string HC = string.Format("{0:x8}", LastChargeDateFromCard);
            try
            {

                LastChargeDateOnCard = DateTime.ParseExact("" + (2000 + Convert.ToInt16(HC.Substring(0, 2))).ToString() + "-" + HC.Substring(2, 2) + "-" + HC.Substring(4, 2) + " " + HC.Substring(6, 2) + ":01:01,111", "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);

            }
            catch (Exception ex)
            {
                string DateFormat = DateTime.Now.Year.ToString() + "-" + string.Format("{0:D2}", DateTime.Now.Month) + "-" + string.Format("{0:D2}", DateTime.Now.Day) + " " + string.Format("{0:D2}", DateTime.Now.Hour) + ":01:01,111";
                LastChargeDateOnCard = DateTime.ParseExact(DateFormat, "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);
            }
            return LastChargeDateOnCard;
        }
        public static DateTime GetDate(uint years, uint months, uint days, uint hours, uint minutes)
        {
            try
            {
                uint Years;
                if (years < 1900 || years > 9999)
                {
                    Years = 2000;
                }
                else
                {
                    Years = years;
                }
                return new DateTime((int)Years, (int)months, (int)days, (int)hours, (int)minutes, 0);
            }
            catch
            {
                return new DateTime(1900, 1, 1);
            }
        }


        public static DateTime GetDate1(int years, int months, int days, int hours, int minutes)
        {
            try
            {
                int Years;
                if (years < 1900 || years > 9999)
                {
                    Years = 2000;
                }
                else
                {
                    Years = years;
                }
                return new DateTime(Years, months, days, hours, minutes, 0);
            }
            catch
            {
                return new DateTime(1900, 1, 1);
            }
        }
    }
    public static class HelperMethods
    {
        public static string SHA1Hashing(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static string HashPassword(string Password)
        {
            return SHA1Hashing(Password + Password + Password);
        }

        // Function to convert  
        // hexadecimal to decimal 
        public static int hexadecimalToDecimal(String hexVal)
        {
            int len = hexVal.Length;

            // Initializing base1 value  
            // to 1, i.e 16^0 
            int base1 = 1;

            int dec_val = 0;

            // Extracting characters as 
            // digits from last character 
            for (int i = len - 1; i >= 0; i--)
            {
                // if character lies in '0'-'9',  
                // converting it to integral 0-9  
                // by subtracting 48 from ASCII value 
                if (hexVal[i] >= '0' &&
                    hexVal[i] <= '9')
                {
                    dec_val += (hexVal[i] - 48) * base1;

                    // incrementing base1 by power 
                    base1 = base1 * 16;
                }

                // if character lies in 'A'-'F' ,  
                // converting it to integral  
                // 10 - 15 by subtracting 55  
                // from ASCII value 
                else if (hexVal[i] >= 'A' &&
                         hexVal[i] <= 'F')
                {
                    dec_val += (hexVal[i] - 55) * base1;

                    // incrementing base1 by power 
                    base1 = base1 * 16;
                }
            }
            return dec_val;
        }
        public static decimal hexadecimalToDecimal2(String hexVal)
        {
            int len = hexVal.Length;

            // Initializing base1 value  
            // to 1, i.e 16^0 
            int base1 = 1;

            decimal dec_val = 0;

            // Extracting characters as 
            // digits from last character 
            for (int i = len - 1; i >= 0; i--)
            {
                // if character lies in '0'-'9',  
                // converting it to integral 0-9  
                // by subtracting 48 from ASCII value 
                if (hexVal[i] >= '0' &&
                    hexVal[i] <= '9')
                {
                    dec_val += (hexVal[i] - 48) * base1;

                    // incrementing base1 by power 
                    base1 = base1 * 16;
                }

                // if character lies in 'A'-'F' ,  
                // converting it to integral  
                // 10 - 15 by subtracting 55  
                // from ASCII value 
                else if (hexVal[i] >= 'A' &&
                         hexVal[i] <= 'F')
                {
                    dec_val += (hexVal[i] - 55) * base1;

                    // incrementing base1 by power 
                    base1 = base1 * 16;
                }
            }
            return dec_val;
        }
    }
}
