using ApiTestingDemo.InsuranceApi.Quotation.ScoreCard;

namespace ApiTestingDemo.InsuranceApi.UnitTests.Quotation.ScoreCard;

public class ScoreCardCalculatorTests
{
    private static DateOnly Today => new(2024, 03, 2);
    private static ApplicantData ApprovedApplicant => new(Today.AddYears(-30), "UK");
    private static int ApprovedCreditScore => 700;

    [Fact]
    public void ShouldApprove()
    {
        var result = ScoreCardCalculator.Calculate(Today, ApprovedApplicant, ApprovedCreditScore);

        Assert.Equal(ScoreCardResult.Approved, result);
    }
    
    [Theory]
    [InlineData(2006, 03, 02)]
    [InlineData(2006, 03, 03)]
    [InlineData(2006, 03, 04)]
    [InlineData(2006, 04, 01)]
    [InlineData(2006, 05, 01)]
    [InlineData(2020, 05, 01)]
    public void ShouldDeclineUnderageApplicants(int year, int month, int day)
    {
        var dateOfBirth = new DateOnly(year, month, day);
        var applicant = ApprovedApplicant with { DateOfBirth = dateOfBirth };

        var result = ScoreCardCalculator.Calculate(Today, applicant, ApprovedCreditScore);

        Assert.Equal(ScoreCardResult.Declined, result);
    }

    [Theory]
    [InlineData(499)]
    [InlineData(498)]
    [InlineData(497)]
    [InlineData(496)]
    [InlineData(495)]
    [InlineData(1)]
    public void ShouldDeclineCreditScoreLessThan500(int creditScore)
    {
        var result = ScoreCardCalculator.Calculate(Today, ApprovedApplicant, creditScore);

        Assert.Equal(ScoreCardResult.Declined, result);
    }
    
    [Theory]
    [InlineData("USA")]
    [InlineData("FR")]
    [InlineData("DE")]
    [InlineData("ES")]
    [InlineData("IT")]
    [InlineData("PT")]
    public void ShouldReferredNoneUk(string nationality)
    {
        var applicant = ApprovedApplicant with { Nationality = nationality };

        var result = ScoreCardCalculator.Calculate(Today, applicant, ApprovedCreditScore);

        Assert.Equal(ScoreCardResult.Referred, result);
    }
}