using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripleDesConsole
{
    public class GlobalHelpers
    {
        public static byte[] CommaSeparatedStringToByteArray(string inString)
        {
            // Split the string 
            string[] csv = inString.Split(',');

            // New an array based on size of csv 
            byte[] byteArray = new byte[csv.Length];    

            // Stuff the bytes from csv into byte array 
            for(int i = 0; i < csv.Length; i++)
                byteArray[i] = Convert.ToByte(csv[i]);

            // Bounce back the result 
            return byteArray;
        }
    }
}
