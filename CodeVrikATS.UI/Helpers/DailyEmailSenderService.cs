

using CodeVrikATS.Entity.Models;

namespace CodeVrikATS.UI.Helpers
{
    public class DailyEmailSenderService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DailyEmailSenderService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Get current date and time
                DateTime currentDateTime = DateTime.Now;

                DateTime targetTime = currentDateTime.Date.AddHours(16).AddMinutes(20);   // 15 = 3:00 PM

                // If current time is past 3:00 PM, set target for tomorrow
                if (currentDateTime > targetTime)
                {
                    targetTime = targetTime.AddDays(1); // move to tomorrow at 3:00 PM
                }

                // Calculate time to wait
                TimeSpan timeToWait = targetTime - currentDateTime;

                // Wait until the target time
                await Task.Delay(timeToWait, stoppingToken);

                // Now execute the task to send email

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var commonService = scope.ServiceProvider.GetRequiredService<CommonService>();

                    // Now safely use your CommonService
                    EmailEntity emailEntity = new EmailEntity
                    {
                        Email = "aman.r@cementdigital.com",
                        Subject = "Daily Report",
                        Body = "This is your daily report."
                    };

                    await commonService.SendEmail(emailEntity);
                }

                // Wait 24 hours before next send
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}
