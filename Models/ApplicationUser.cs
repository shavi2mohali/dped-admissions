using Microsoft.AspNetCore.Identity;

namespace DPEDAdmissionSystem.Models;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public StudentApplication? Application { get; set; }
}
