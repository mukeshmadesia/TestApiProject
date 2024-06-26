using dotenv.net;

#pragma warning disable CS8603

public static class Config
{
    static Config()
    {
        //DotEnv.Load(); // This loads the .env file
        DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
    }

    public static string ApiBaseUrl => Environment.GetEnvironmentVariable("API_BASE_URL");
    public static string AuthorizationKey => Environment.GetEnvironmentVariable("AUTHORIZATION_KEY");
}
