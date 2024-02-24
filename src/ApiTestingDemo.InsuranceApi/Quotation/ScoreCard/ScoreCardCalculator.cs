namespace ApiTestingDemo.InsuranceApi.Quotation.ScoreCard;

public static class ScoreCardCalculator
{
    public static ScoreCardResult Calculate(
        DateOnly today,
        ApplicantData applicant,
        int creditScore
    )
    {
        var age = CalculateAge(applicant, today);
        if (age < 18)
        {
            return ScoreCardResult.Declined;
        }

        if (creditScore < 500)
        {
            return ScoreCardResult.Declined;
        }
        
        if(applicant.Nationality != "UK")
        {
            return ScoreCardResult.Referred;
        }
        
        return ScoreCardResult.Approved;
    }

    private static int CalculateAge(ApplicantData applicant, DateOnly today)
    {
        var age = today.Year - applicant.DateOfBirth.Year;
        if (today.Month < applicant.DateOfBirth.Month ||
            (today.Month == applicant.DateOfBirth.Month && today.Day <= applicant.DateOfBirth.Day))
        {
            age--;
        }

        return age;
    }
}