using DPEDAdmissionSystem.Models;

namespace DPEDAdmissionSystem.Services;

public interface IPaymentService
{
    Task<Payment> MarkPaidAsync(StudentApplication application, decimal amount);
}
