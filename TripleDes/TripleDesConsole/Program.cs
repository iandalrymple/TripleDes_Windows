using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

class Program
{
    // Here is the link that was used for setting up the logging.
    // https://www.blinkingcaret.com/2018/02/14/net-core-console-logging/#:~:text=This%20blog%20post%20will%20walk%20you%20through%20setting,add%20package%20Serilog.Sinks.File%20And%20then%20in%20Program.cs%E2%80%99%20Main%3A

    // Link for db context in console application 
    // http://www.techtutorhub.com/article/How-to-Add-Entity-Framework-Core-DBContext-in-Dot-NET-Core-Console-Application/86#:~:text=%20How%20to%20Add%20Entity%20Framework%20Core%20DBContext,the%20DbContext%20and%20Models%20from%20already...%20More%20

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