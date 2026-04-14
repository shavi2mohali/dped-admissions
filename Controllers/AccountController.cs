using Microsoft.AspNetCore.Mvc;
using DPEDAdmissionSystem.Models;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        // TODO: Save to database

        if (request.Email == "test@test.com")
            return BadRequest("User already exists");

        return Ok("Registered successfully");
    }
}