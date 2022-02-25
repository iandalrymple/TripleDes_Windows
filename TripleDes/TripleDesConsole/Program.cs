using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Security.Cryptography;
using TripleDesConsole;

class Program
{
    // Constants 
    const int IDX_TYPE      = 0;
    const int IDX_IN_FILE   = 1;
    const int IDX_OUT_FILE  = 3;

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

        //// Register classes with logger 
        //serviceCollection.AddLogging(configure => configure.AddSerilog())
        //                        .AddSingleton<RestoreDatabase>()
        //                        .AddSingleton<EmailWrapper>();

        // Create the provider 
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Create the logger for Program class 
        var logger = serviceProvider?.GetService<ILogger<Program>>();

        try
        {
            // Log out here that we have started the application 
            logger?.LogInformation("Starting the application.");


            



            // Decide what we want to do based on first argument 
            if (args[IDX_TYPE] == "ENCRYPT")
            {
                // Encrypt the contents of the file 
                EncryptionHelpers.EncryptTextToFile(args[IDX_IN_FILE], File.ReadAllText(args[IDX_OUT_FILE]);

                // Read in the file we want to encrypt 
                string inContents = File.ReadAllText(args[IDX_IN_FILE]);

                // Encrypt the inContents 
                inContents = EncryptPlainTextToCipherText(inContents, args[IDX_PASSWORD]);

                // Now write back out to a file 
                File.WriteAllText(args[IDX_OUT_FILE], inContents);

                // Print the file out again after parsing just to make sure nothing got messed up 
                Console.WriteLine(DecryptCipherTextToPlainText(File.ReadAllText(args[IDX_OUT_FILE]), args[IDX_PASSWORD]));
            }
            //else
            //{
            //    // Read in the file we want to decrypt 
            //    string inContents = File.ReadAllText(args[IDX_IN_FILE]);

            //    // Decrypt the inContents 
            //    inContents = DecryptCipherTextToPlainText(inContents, args[IDX_PASSWORD]);

            //    // Now write back out to a file 
            //    File.WriteAllText(args[IDX_OUT_FILE], inContents);

            //    // Print the file out again after parsing just to make sure nothing got messed up 
            //    Console.WriteLine(inContents);
            //}

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