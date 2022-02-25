using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using TripleDesConsole;

class Program
{
    // Constants 
    const int IDX_TYPE      = 0;        // ENCRYPT or DECRYPT
    const int IDX_IN_FILE   = 1;        // Contains the raw data 
    const int IDX_OUT_FILE  = 2;        // New file with resulting data 

    static void Main(string[] args)
    {
        // Grab the config in case we need to inject 
        IConfiguration configuration = new ConfigurationBuilder()
                                           .AddJsonFile("appsettings.json")
                                           .Build();
        // Configure the logger 
        Log.Logger = new LoggerConfiguration()
                        .WriteTo.File("AppLogs.log")
                        .CreateLogger();

        // Create the collection
        var serviceCollection = new ServiceCollection();

        // Register the configuration
        serviceCollection.AddSingleton(configuration);

        // Create the provider 
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Create the logger for Program class 
        var logger = serviceProvider?.GetService<ILogger<Program>>();

        try
        {
            // Log out here that we have started the application 
            logger?.LogInformation("Starting the application.");

            // Get the key and IV 
            byte[] desKey = GlobalHelpers.CommaSeparatedStringToByteArray(SecretManager.ReadSecretString("DesKey"));
            byte[] desIv = GlobalHelpers.CommaSeparatedStringToByteArray(SecretManager.ReadSecretString("InitVector"));

            // Make sure the outfile is blown out 
            if(File.Exists(args[IDX_OUT_FILE]))
                File.Delete(args[IDX_OUT_FILE]);

            // Decide what we want to do based on first argument 
            if (args[IDX_TYPE] == "ENCRYPT")
            {
                // Encrypt the contents of the file 
                EncryptionHelpers.EncryptTextToFile(args[IDX_IN_FILE], args[IDX_OUT_FILE], desKey, desIv);

                // Print the file out again after parsing just to make sure nothing got messed up 
                Console.WriteLine(EncryptionHelpers.DecryptTextFromFile(args[IDX_OUT_FILE], desKey, desIv));
            }
            else
            {
                // Get the string of the decrypted file 
                string decryptedFile = EncryptionHelpers.DecryptTextFromFile(args[IDX_IN_FILE], desKey, desIv);

                // Write it to file 
                using (StreamWriter outputFile = new StreamWriter(args[IDX_OUT_FILE]))
                    outputFile.WriteLine(decryptedFile);

                // Print out the results from the file as a double check 
                Console.WriteLine(File.ReadAllText(args[IDX_OUT_FILE]));
            }

            // Log out here that we have completed the application 
            logger?.LogInformation("Completing the application.");
        }
        catch (Exception ex)
        {
            // Log to console  
            Console.WriteLine(ex.ToString());

            // Log to file
            logger?.LogError(ex, "Program:Main:Exception");
        }
    }
}