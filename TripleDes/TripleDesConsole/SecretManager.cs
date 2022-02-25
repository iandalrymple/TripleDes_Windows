using System.Text.Json;

namespace TripleDesConsole
{
    public static class SecretManager
    {
        private static readonly string SECRET_FILE_NAME = "Credentials.json";

        public static string ReadSecretString(string? inKey)
        {
            // Local 
            string returnValue = "";

            // Just grab the text of the whole file 
            string text = File.ReadAllText(SECRET_FILE_NAME);

            // Get the secrets 
            Secrets? secrets = JsonSerializer.Deserialize<Secrets>(text);

            // Make sure not null here 
            if (secrets != null)
            {
                // Map and get the result 
                switch (inKey)
                {
                    case "DesKey":
                        if (secrets.DesKey != null)
                            returnValue = secrets.DesKey;
                        break;
                    case "InitVector":
                        if (secrets.InitVector != null)
                            returnValue = secrets.InitVector;
                        break;
                    default:
                        returnValue = "";
                        break;
                }
            }

            // Bounce back the result 
            return returnValue;
        }
    }

    public class Secrets
    {
        public string? DesKey { get; set; }
        public string? InitVector { get; set; }
    }
}
