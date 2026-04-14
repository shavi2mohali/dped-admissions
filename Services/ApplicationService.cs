using DPEDAdmissionSystem.Data;
using DPEDAdmissionSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DPEDAdmissionSystem.Services;

public class ApplicationService(ApplicationDbContext context) : IApplicationService
{
    public async Task<StudentApplication> GetOrCreateApplicationAsync(ApplicationUser user)
    {
        var application = await context.Applications
            .Include(x => x.AcademicDetail)
            .Include(x => x.Payment)
            .FirstOrDefaultAsync(x => x.UserId == user.Id);

        if (application is not null)
        {
            return application;
        }

        application = new StudentApplication
        {
            UserId = user.Id,
            CandidateName = user.FullName,
            Email = user.Email ?? string.Empty,
            MobileNumber = user.PhoneNumber ?? string.Empty,
            DateOfBirth = DateTime.Today.AddYears(-18),
            Gender = GenderType.Other,
            MaritalStatus = MaritalStatusType.Single,
            Category = CategoryType.General,
            AcademicDetail = new AcademicDetail()
        };

        context.Applications.Add(application);
        await context.SaveChangesAsync();
        return application;
    }

    public async Task<StudentApplication?> GetApplicationByUserIdAsync(string userId)
    {
        return await context.Applications
            .Include(x => x.AcademicDetail)
            .Include(x => x.Payment)
            .FirstOrDefaultAsync(x => x.UserId == userId);
    }

    public async Task<StudentApplication?> GetApplicationWithDetailsAsync(int applicationId)
    {
        return await context.Applications
            .Include(x => x.User)
            .Include(x => x.AcademicDetail)
            .Include(x => x.Payment)
            .FirstOrDefaultAsync(x => x.Id == applicationId);
    }

    public async Task SaveApplicationAsync(StudentApplication application)
    {
        application.UpdatedOn = DateTime.UtcNow;

        if (application.Id == 0)
        {
            context.Applications.Add(application);
        }
        else
        {
            context.Applications.Update(application);
            context.AcademicDetails.Update(application.AcademicDetail);
        }

        await context.SaveChangesAsync();
    }

    public decimal CalculateFee(CategoryType category) => category switch
    {
        CategoryType.SCRandO => 300,
        CategoryType.SCMandB => 300,
        CategoryType.PhysicallyHandicapped => 300,
        CategoryType.ExServiceman => 0,
        _ => 600
    };

    public async Task<string> EnsureRegistrationNumberAsync(StudentApplication application)
    {
        if (!string.IsNullOrWhiteSpace(application.RegistrationNumber))
        {
            return application.RegistrationNumber;
        }

        var year = DateTime.Now.Year;
        var count = await context.Applications.CountAsync(x => x.RegistrationNumber != null && x.RegistrationNumber.StartsWith($"DPE-{year}-"));
        application.RegistrationNumber = $"DPE-{year}-{(count + 1):0000}";
        application.Status = ApplicationStatus.Completed;

        context.Applications.Update(application);
        await context.SaveChangesAsync();

        return application.RegistrationNumber;
    }

    public async Task<List<StudentApplication>> SearchApplicationsAsync(string? nameOrRegistration, CategoryType? category, PaymentStatus? paymentStatus)
    {
        var query = context.Applications
            .Include(x => x.Payment)
            .Include(x => x.AcademicDetail)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(nameOrRegistration))
        {
            var search = nameOrRegistration.Trim().ToLower();
            query = query.Where(x =>
                x.CandidateName.ToLower().Contains(search) ||
                (x.RegistrationNumber != null && x.RegistrationNumber.ToLower().Contains(search)));
        }

        if (category.HasValue)
        {
            query = query.Where(x => x.Category == category.Value);
        }

        if (paymentStatus.HasValue)
        {
            query = query.Where(x => x.Payment != null && x.Payment.Status == paymentStatus.Value);
        }

        return await query.OrderByDescending(x => x.CreatedOn).ToListAsync();
    }
}
