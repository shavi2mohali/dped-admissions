namespace DPEDAdmissionSystem.Services;

public interface INotificationService
{
    Task SendAsync(string mobileNumber, string message);
}
