using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace MovieRecommender.WebAPI.Extensions
{
    public static class LoggerBuilder
    {
        public static void Build(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ApplicationSQL");
            string dateTime = DateTime.Now.ToShortDateString();

            MSSqlServerSinkOptions sqlOptions = new MSSqlServerSinkOptions()
            {
                TableName = "Logs",
                AutoCreateSqlTable = true,
            };

            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Information()
                            .WriteTo.Console()
                            .WriteTo.MSSqlServer(connectionString, sinkOptions: sqlOptions, restrictedToMinimumLevel: LogEventLevel.Error)
                            .WriteTo.File($"logs/log_{dateTime}.txt", LogEventLevel.Information)
                            .Enrich.FromLogContext()
                            .CreateLogger();

        }
    }
}
