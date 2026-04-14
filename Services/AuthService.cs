using System.Net.Http.Json;
using DPEDAdmissionSystem.Models;

namespace DPEDAdmissionSystem.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<(bool Success, string Message)> Register(RegisterRequest request)
        {
            var response = await _http.PostAsJsonAsync("/account/register", request);

            if (response.IsSuccessStatusCode)
                return (true, "Registration successful");

            var error = await response.Content.ReadAsStringAsync();
            return (false, error);
        }
    }
}