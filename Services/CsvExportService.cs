using System.Text;
using DPEDAdmissionSystem.Models;

namespace DPEDAdmissionSystem.Services;

public class CsvExportService : ICsvExportService
{
    public byte[] ExportApplications(List<StudentApplication> applications)
    {
        var builder = new StringBuilder();
        builder.AppendLine("RegistrationNumber,CandidateName,Category,PaymentStatus,Fee,MobileNumber,Email");

        foreach (var app in applications)
        {
            var registration = Escape(app.RegistrationNumber ?? string.Empty);
            var name = Escape(app.CandidateName);
            var category = Escape(app.Category);
            var paymentStatus = Escape(app.Payment?.Status.ToString() ?? "Pending");
            var amount = app.Payment?.Amount.ToString("0.00") ?? "0.00";
            var mobile = Escape(app.MobileNumber);
            var email = Escape(app.Email);

            builder.AppendLine($"{registration},{name},{category},{paymentStatus},{amount},{mobile},{email}");
        }

        return Encoding.UTF8.GetBytes(builder.ToString());
    }

    private static string Escape(string value) => $"\"{value.Replace("\"", "\"\"")}\"";
}
