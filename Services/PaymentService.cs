using DPEDAdmissionSystem.Data;
using DPEDAdmissionSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DPEDAdmissionSystem.Services;

public class PaymentService(
    ApplicationDbContext context,
    INotificationService notificationService,
    IApplicationService applicationService) : IPaymentService
{
    public async Task<Payment> MarkPaidAsync(StudentApplication application, decimal amount)
    {
        var payment = application.Payment ?? new Payment
        {
            StudentApplicationId = application.Id
        };

        payment.Amount = amount;
        payment.Status = PaymentStatus.Success;
        payment.PaidOn = DateTime.UtcNow;
        payment.TransactionId = $"TXN-{DateTime.UtcNow:yyyyMMddHHmmss}-{application.Id}";

        application.Payment = payment;
        application.Status = amount == 0 ? ApplicationStatus.Completed : ApplicationStatus.Paid;

        if (context.Entry(payment).State == EntityState.Detached)
        {
            context.Payments.Add(payment);
        }
        else
        {
            context.Payments.Update(payment);
        }

        context.Applications.Update(application);
        await context.SaveChangesAsync();

        await applicationService.EnsureRegistrationNumberAsync(application);
        await notificationService.SendAsync(application.MobileNumber, $"Payment received for DPED application. Registration No: {application.RegistrationNumber}");

        return payment;
    }
}
