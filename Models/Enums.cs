namespace DPEDAdmissionSystem.Models;

public enum CategoryType
{
    General,
    SCRandO,
    SCMandB,
    BC,
    EWS,
    ExServiceman,
    Sports,
    PhysicallyHandicapped
}

public enum MaritalStatusType
{
    Single,
    Married,
    Other
}

public enum GenderType
{
    Male,
    Female,
    Other
}

public enum ApplicationStatus
{
    Pending,
    Paid,
    Completed
}

public enum PaymentStatus
{
    Pending,
    Success,
    Failed
}
