using DPEDAdmissionSystem.Models;

namespace DPEDAdmissionSystem.Services;

public interface IApplicationService
{
    Task<StudentApplication> GetOrCreateApplicationAsync(ApplicationUser user);
    Task<StudentApplication?> GetApplicationByUserIdAsync(string userId);
    Task<StudentApplication?> GetApplicationWithDetailsAsync(int applicationId);
    Task SaveApplicationAsync(StudentApplication application);
    decimal CalculateFee(CategoryType category);
    Task<string> EnsureRegistrationNumberAsync(StudentApplication application);
    Task<List<StudentApplication>> SearchApplicationsAsync(string? nameOrRegistration, CategoryType? category, PaymentStatus? paymentStatus);
}
