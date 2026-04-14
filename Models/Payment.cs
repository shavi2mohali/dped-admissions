using System.ComponentModel.DataAnnotations;

namespace DPEDAdmissionSystem.Models;

public class Payment
{
    public int Id { get; set; }

    [Required]
    public int StudentApplicationId { get; set; }
    public StudentApplication? StudentApplication { get; set; }

    [Range(0, 10000)]
    public decimal Amount { get; set; }

    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    [StringLength(50)]
    public string TransactionId { get; set; } = string.Empty;

    public DateTime? PaidOn { get; set; }
}
