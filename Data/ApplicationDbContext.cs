using DPEDAdmissionSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DPEDAdmissionSystem.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<StudentApplication> Applications => Set<StudentApplication>();
    public DbSet<AcademicDetail> AcademicDetails => Set<AcademicDetail>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<StudentApplication>()
            .HasOne(x => x.User)
            .WithOne(x => x.Application)
            .HasForeignKey<StudentApplication>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<StudentApplication>()
            .HasOne(x => x.AcademicDetail)
            .WithOne(x => x.StudentApplication)
            .HasForeignKey<AcademicDetail>(x => x.StudentApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<StudentApplication>()
            .HasOne(x => x.Payment)
            .WithOne(x => x.StudentApplication)
            .HasForeignKey<Payment>(x => x.StudentApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<StudentApplication>()
            .HasIndex(x => x.RegistrationNumber)
            .IsUnique()
            .HasFilter("[RegistrationNumber] IS NOT NULL");

        builder.Entity<StudentApplication>(entity =>
        {
            entity.Property(x => x.Gender)
                .HasMaxLength(20);

            entity.Property(x => x.MaritalStatus)
                .HasMaxLength(20);

            entity.Property(x => x.Category)
                .HasMaxLength(50);
        });
    }
}
