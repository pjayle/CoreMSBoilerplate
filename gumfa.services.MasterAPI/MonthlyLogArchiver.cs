namespace gumfa.services.MasterAPI
{
    using System.IO.Compression;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class MonthlyLogArchiver : BackgroundService
    {
        private readonly ILogger<MonthlyLogArchiver> _logger;
        private readonly string _logDirectory = "Logs";
        private readonly string _archiveDirectory = "ArchivedLogs";

        public MonthlyLogArchiver(ILogger<MonthlyLogArchiver> logger)
        {
            _logger = logger;
            Directory.CreateDirectory(_logDirectory);
            Directory.CreateDirectory(_archiveDirectory);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;

                // Check if it's the 1st day of the month and it's midnight
                if (now.Day == 1 && now.Hour == 0)
                {
                    try
                    {
                        string lastMonth = now.AddMonths(-1).ToString("yyyy-MM");
                        var logFiles = Directory.GetFiles(_logDirectory, $"log-{lastMonth}-*.txt");

                        if (logFiles.Length > 0)
                        {
                            string zipPath = Path.Combine(_archiveDirectory, $"Logs_{lastMonth}.zip");

                            // Delete zip if already exists
                            if (File.Exists(zipPath))
                                File.Delete(zipPath);

                            using (var zip = ZipFile.Open(zipPath, ZipArchiveMode.Create))
                            {
                                foreach (var file in logFiles)
                                {
                                    zip.CreateEntryFromFile(file, Path.GetFileName(file));
                                }
                            }

                            _logger.LogInformation($"✅ Archived {logFiles.Length} logs into {zipPath}");
                        }
                        else
                        {
                            _logger.LogInformation($"ℹ️ No logs found for {lastMonth} to archive.");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "❌ Error archiving log files.");
                    }

                    // Sleep for 1 hour so it doesn't rerun multiple times that day
                    await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                }
                else
                {
                    // Check again every 30 minutes
                    await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
                }
            }
        }
    }

}
