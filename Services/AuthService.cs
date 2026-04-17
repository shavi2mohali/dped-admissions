using System.Net.Http.Json;
using DPEDAdmissionSystem.Models;
using Microsoft.AspNetCore.Components;

namespace DPEDAdmissionSystem.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            if (_http.BaseAddress == null)
            {
                _http.BaseAddress = new Uri(navigationManager.BaseUri);
            }
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