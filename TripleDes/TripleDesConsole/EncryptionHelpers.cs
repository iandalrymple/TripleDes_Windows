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
            }
        }

        public static string DecryptTextFromFile(String FileName, byte[] Key, byte[] IV)
        {
            try
            {
                string retVal = "";
                // Create or open the specified file.
                using (FileStream fStream = File.Open(FileName, FileMode.OpenOrCreate))
                {

                    // Create a new TripleDES object.
                    using (TripleDES tripleDESalg = TripleDES.Create())
                    {

                        // Create a CryptoStream using the FileStream
                        // and the passed key and initialization vector (IV).
                        using (CryptoStream cStream = new CryptoStream(fStream,
                            tripleDESalg.CreateDecryptor(Key, IV),
                            CryptoStreamMode.Read))
                        {

                            // Create a StreamReader using the CryptoStream.
                            using (StreamReader sReader = new StreamReader(cStream))
                            {

                                // Read the data from the stream
                                // to decrypt it.
                                retVal = sReader.ReadLine();
                            }
                        }
                    }
                }
                // Return the string.
                return retVal;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("A file access error occurred: {0}", e.Message);
                return null;
            }
        }




        ////This method is used to convert the plain text to Encrypted/Un-Readable Text format.
        //public static string EncryptPlainTextToCipherText(string PlainText, string SecurityKey)
        //{
        //    // Getting the bytes of Input String.
        //    byte[] toEncryptedArray = UTF8Encoding.UTF8.GetBytes(PlainText);

        //    MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();
        //    //Gettting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value.
        //    byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(SecurityKey));
        //    //De-allocatinng the memory after doing the Job.
        //    objMD5CryptoService.Clear();

        //    var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();
        //    //Assigning the Security key to the TripleDES Service Provider.
        //    objTripleDESCryptoService.Key = securityKeyArray;
        //    //Mode of the Crypto service is Electronic Code Book.
        //    objTripleDESCryptoService.Mode = CipherMode.ECB;
        //    //Padding Mode is PKCS7 if there is any extra byte is added.
        //    objTripleDESCryptoService.Padding = PaddingMode.PKCS7;


        //    var objCrytpoTransform = objTripleDESCryptoService.CreateEncryptor();
        //    //Transform the bytes array to resultArray
        //    byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);
        //    objTripleDESCryptoService.Clear();
        //    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        //}

        ////This method is used to convert the Encrypted/Un-Readable Text back to readable  format.
        //public static string DecryptCipherTextToPlainText(string CipherText, string SecurityKey)
        //{
        //    byte[] toEncryptArray = Convert.FromBase64String(CipherText);
        //    MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();

        //    //Gettting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value.
        //    byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(SecurityKey));
        //    objMD5CryptoService.Clear();

        //    var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();
        //    //Assigning the Security key to the TripleDES Service Provider.
        //    objTripleDESCryptoService.Key = securityKeyArray;
        //    //Mode of the Crypto service is Electronic Code Book.
        //    objTripleDESCryptoService.Mode = CipherMode.ECB;
        //    //Padding Mode is PKCS7 if there is any extra byte is added.
        //    objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

        //    var objCrytpoTransform = objTripleDESCryptoService.CreateDecryptor();
        //    //Transform the bytes array to resultArray
        //    byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        //    objTripleDESCryptoService.Clear();

        //    //Convert and return the decrypted data/byte into string format.
        //    return UTF8Encoding.UTF8.GetString(resultArray);
        //}

    }
}
