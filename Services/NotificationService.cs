using Microsoft.Extensions.Logging;

namespace DPEDAdmissionSystem.Services;

public class NotificationService(ILogger<NotificationService> logger) : INotificationService
{
    public Task SendAsync(string mobileNumber, string message)
    {
        logger.LogInformation("Simulated SMS to {Mobile}: {Message}", mobileNumber, message);
        return Task.CompletedTask;
    }
}
