namespace BLL
{
    public enum LogLevel
    {
        Info,
        Warn,
        Error,
        Fatal,
    }

    public static class Logger
    {
        public static void WriteLog(string message, LogLevel logLevel)
        {
            string logPath = "../../../../logs/logs.txt";

            CheckFilePath(logPath);

            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine($"{DateTime.Now} {GetStringLogLevel(logLevel)}: {message}");
            }
        }

        private static void CheckFilePath(string path)
        {
            string? logDirectory = Path.GetDirectoryName(path);
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            if (!File.Exists(path))
            {
                using (FileStream fs = File.Create(path)) { }
            }
        }

        private static string GetStringLogLevel(LogLevel logLevel)
        {
            string logLevelString;

            switch (logLevel)
            {
                case LogLevel.Info:
                    logLevelString = "Info";
                    break;
                case LogLevel.Warn:
                    logLevelString = "Warn";
                    break;
                case LogLevel.Error:
                    logLevelString = "Error";
                    break;
                case LogLevel.Fatal:
                    logLevelString = "Fatal";
                    break;
                default:
                    logLevelString = "Underfined";
                    break;
            }

            return logLevelString;
        }
    }
}
