using Microsoft.AspNetCore.Mvc;
using DPEDAdmissionSystem.Models;
using DPEDAdmissionSystem.Services;
using Microsoft.AspNetCore.Identity;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly INotificationService _notificationService;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        INotificationService notificationService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _notificationService = notificationService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (request.Password != request.ConfirmPassword)
        {
            return BadRequest("Passwords do not match");
        }

        var user = new ApplicationUser
        {
            FullName = request.Name,
            UserName = request.Email,
            Email = request.Email,
            PhoneNumber = request.MobileNumber,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var error = string.Join(" ", result.Errors.Select(x => x.Description));
            return BadRequest(error);
        }

        await _userManager.AddToRoleAsync(user, "Student");
        await _notificationService.SendAsync(request.MobileNumber, $"Welcome {request.Name}, your DPED registration account has been created.");
        await _signInManager.SignInAsync(user, isPersistent: false);
        return Ok("Registered successfully");
    }
}
