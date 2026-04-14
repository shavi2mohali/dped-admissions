using System.ComponentModel.DataAnnotations;

namespace DPEDAdmissionSystem.Models;

public class StudentApplication
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser? User { get; set; }

    [Required]
    [StringLength(150)]
    public string CandidateName { get; set; } = string.Empty;

    [Required]
    [StringLength(150)]
    public string FatherName { get; set; } = string.Empty;

    [Required]
    [StringLength(150)]
    public string MotherName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Date of birth is required")]
    public DateTime? DateOfBirth { get; set; } = DateTime.Today.AddYears(-18);

    [Required(ErrorMessage = "Gender is required")]
    public GenderType? Gender { get; set; }

    [Required(ErrorMessage = "Marital status is required")]
    public MaritalStatusType? MaritalStatus { get; set; }

    [Required(ErrorMessage = "Category is required")]
    public CategoryType? Category { get; set; }

    [Required]
    [StringLength(300)]
    public string Address { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Tehsil { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string District { get; set; } = string.Empty;

    [Required]
    [StringLength(10)]
    public string PinCode { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Mobile number must be exactly 10 digits")]
    public string MobileNumber { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;

    [StringLength(20)]
    public string? RegistrationNumber { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedOn { get; set; }

    public AcademicDetail AcademicDetail { get; set; } = new();
    public Payment? Payment { get; set; }
}
