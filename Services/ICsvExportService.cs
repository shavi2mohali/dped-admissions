using DPEDAdmissionSystem.Models;

namespace DPEDAdmissionSystem.Services;

public interface ICsvExportService
{
    byte[] ExportApplications(List<StudentApplication> applications);
}
