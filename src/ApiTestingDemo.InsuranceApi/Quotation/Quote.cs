using System.Diagnostics.CodeAnalysis;
using ApiTestingDemo.InsuranceApi.Quotation.ScoreCard;

namespace ApiTestingDemo.InsuranceApi.Quotation;

public record Quote
{
    private Quote(Guid id, Applicant applicant, DateOnly from, int duration)
    {
        Id = id;
        Applicant = applicant;
        From = from;
        Duration = duration;
    }

    public Guid Id { get; }
    public Applicant Applicant { get; }
    public DateOnly From { get; }
    public int Duration { get; }
    public int CreditScore { get; private set; }
    public ScoreCardResult? ScoreCardResult { get; private set; }

    public QuoteResult Result => ScoreCardResult switch
    {
        InsuranceApi.ScoreCardResult.Approved => QuoteResult.Approved,
        InsuranceApi.ScoreCardResult.Referred => QuoteResult.Referred,
        null or InsuranceApi.ScoreCardResult.Declined => QuoteResult.Declined,
        _ => throw new ArgumentOutOfRangeException()
    };

    public void CalculateQuotation(DateOnly today, int creditScore)
    {
        var scoreCardResult = ScoreCardCalculator.Calculate(
            today,
            new ApplicantData(
                Applicant.DateOfBirth,
                Applicant.Nationality),
            creditScore
        );

        CreditScore = creditScore;
        ScoreCardResult = scoreCardResult;
    }

    public static bool TryCreate(
        DateOnly today,
        Applicant applicant,
        DateOnly from,
        int duration,
        [NotNullWhen(true)] out Quote? quote,
        [NotNullWhen(false)] out string? errorMessage)
    {
        quote = null;
        errorMessage = null;

        if (from < today)
        {
            errorMessage = "The quote start date must be in the future.";
            return false;
        }

        if (duration is <= 0 or > 365)
        {
            errorMessage = "The quote duration must be between 1 and 365 days.";
            return false;
        }

        quote = new Quote(
            Guid.NewGuid(),
            applicant,
            from,
            duration
        );

        return true;
    }
}