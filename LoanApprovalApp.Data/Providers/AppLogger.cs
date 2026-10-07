namespace LoanApprovalApp.Data.Providers
{
    public static class AppLogger
    {
        private static readonly string logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app_log.txt");

        public static void Log(string message)
        {
            try
            {
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}";
                File.AppendAllText(logFile, logEntry);
            }
            catch
            {
            }
        }
    }
}