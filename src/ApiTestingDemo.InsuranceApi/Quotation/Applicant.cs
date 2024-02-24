namespace ApiTestingDemo.InsuranceApi.Quotation;

public record Applicant(
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    EyeColor EyeColor,
    int Height,
    string Nationality
);