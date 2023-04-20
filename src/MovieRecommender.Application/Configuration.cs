using Microsoft.Extensions.Configuration;

namespace MovieRecommender.Application
{
    public static class Configuration
    {
        private static ConfigurationManager ConfigurationManager
        {
            get
            {
                ConfigurationManager configuration = new();
                configuration.SetBasePath(Directory.GetCurrentDirectory());
                configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return configuration;
            }
        }
        static public string ConnectionString => ConfigurationManager.GetConnectionString("ApplicationSQL") ?? throw new NullReferenceException("Connection string bulunamadı!");

        static public string Api_Key => ConfigurationManager.GetSection("api_key").Value ?? throw new NullReferenceException("Api key bulunamadı!");
        static public double TimerTickFromMinute => Convert.ToDouble(ConfigurationManager.GetSection("TimerTickFromMinute").Value);
        static public int MovieRecordCount => Convert.ToInt32(ConfigurationManager.GetSection("MovieRecordCount").Value);
    }
}
