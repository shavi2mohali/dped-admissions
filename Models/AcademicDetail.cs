using System.ComponentModel.DataAnnotations;

namespace DPEDAdmissionSystem.Models;

public class AcademicDetail
{
    public int Id { get; set; }

    [Required]
    public int StudentApplicationId { get; set; }
    public StudentApplication? StudentApplication { get; set; }

    [Required]
    [StringLength(50)]
    public string TenthRollNumber { get; set; } = string.Empty;

    [Required]
    public int TenthYearOfPassing { get; set; }

    [Required]
    [StringLength(150)]
    public string TenthBoardName { get; set; } = string.Empty;

    [Range(0, 9999)]
    public decimal TenthTotalMarks { get; set; }

    [Range(0, 100)]
    public decimal TenthPercentage { get; set; }

    [Required]
    public bool HasEnglish { get; set; } = true;

    [Required]
    public bool HasHindi { get; set; } = true;

    [Required]
    public bool HasMaths { get; set; } = true;

    [Required]
    public bool HasScience { get; set; } = true;

    [Required]
    public bool HasSocialStudies { get; set; } = true;

    [Required]
    [StringLength(50)]
    public string TwelfthRollNumber { get; set; } = string.Empty;

    [Required]
    public int TwelfthYearOfPassing { get; set; }

    [Required]
    [StringLength(150)]
    public string TwelfthBoardOrUniversity { get; set; } = string.Empty;

    [Range(0, 9999)]
    public decimal TwelfthTotalMarks { get; set; }

    [Range(0, 100)]
    public decimal TwelfthPercentage { get; set; }
}
