namespace ApiTestingDemo.InsuranceApi.Quotation;

public record ResourceRepresentationApplicant(
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    EyeColor EyeColor,
    int Height,
    string Nationality)
{
    public Applicant ToApplicant()
    {
        return new Applicant(
            FirstName,
            LastName,
            DateOfBirth,
            EyeColor,
            Height,
            Nationality
        );
    }

    public static ResourceRepresentationApplicant FromApplicant(Applicant quoteApplicant)
    {
        return new(
            quoteApplicant.FirstName,
            quoteApplicant.LastName,
            quoteApplicant.DateOfBirth,
            quoteApplicant.EyeColor,
            quoteApplicant.Height,
            quoteApplicant.Nationality
        );
    }
}