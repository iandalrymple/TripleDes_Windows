using System.Security.Cryptography;

namespace TripleDesConsole
{
    public static class EncryptionHelpers
    {
        // Content taken from here 
        // https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.tripledes.create?view=net-6.0

        public static void EncryptTextToFile(string inFile, string outFile, byte[] inKey, byte[] inVector)
        {
            try
            {
                // First read in all the data from the infile 
                string allInFileText = File.ReadAllText(inFile);

                // Blow out the outfile if it exists 
                if(File.Exists(outFile))   
                    File.Delete(outFile);

                // Create or open the specified file.
                using (FileStream fStream = File.Open(outFile, FileMode.OpenOrCreate))
                {
                    // Create a new TripleDES object.
                    using (TripleDES tripleDESalg = TripleDES.Create())
                    {
                        // Create a CryptoStream using the FileStream and the passed key and initialization vector (IV).
                        using (CryptoStream cStream = new CryptoStream(fStream, tripleDESalg.CreateEncryptor(inKey, inVector), CryptoStreamMode.Write))
                        {
                            // Create a StreamWriter using the CryptoStream.
                            using (StreamWriter sWriter = new StreamWriter(cStream))
                            {
                                // Write the data to the stream to encrypt it.
                                sWriter.WriteLine(allInFileText);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Just write it all out 
                Console.WriteLine(ex);

                // Toss it up 
                throw;
            }
        }

        public static string DecryptTextFromFile(string inFile, byte[] inKey, byte[] inVector)
        {
            try
            {
                // Locals 
                string retVal = "";

                // Create or open the specified file.
                using (FileStream fStream = File.Open(inFile, FileMode.OpenOrCreate))
                {
                    // Create a new TripleDES object.
                    using (TripleDES tripleDESalg = TripleDES.Create())
                    {
                        // Create a CryptoStream using the FileStream and the passed key and initialization vector (IV).
                        using (CryptoStream cStream = new CryptoStream(fStream, tripleDESalg.CreateDecryptor(inKey, inVector), CryptoStreamMode.Read))
                        {
                            // Create a StreamReader using the CryptoStream.
                            using (StreamReader sReader = new StreamReader(cStream))
                            {  
                                // Read the data from the stream to decrypt it.
                                retVal = sReader.ReadToEnd();
                            }
                        }
                    }
                }

                // Return the string.
                return retVal;
            }
            catch (Exception ex)
            {
                // Just write it all out 
                Console.WriteLine(ex);

                // Toss it up 
                throw;
            }
        }
    }
}
