using ApiTestingDemo.InsuranceApi;
using ApiTestingDemo.InsuranceApi.Quotation;
using FluentAssertions;
using MongoDB.Bson;

namespace ApiTestingDemo.FunctionalTests.Quotation;

public static class BsonDocumentQuoteAssertions
{
    public static void Assert(BsonDocument document,
        Guid id, QuoteResourceRepresentation quote,
        int creditScore,
        ScoreCardResult? scoreCardResult = null)
    {
        document["_id"].AsGuid.Should().Be(id);
        document["Applicant"]["FirstName"].AsString.Should().Be(quote.Applicant.FirstName);
        document["Applicant"]["LastName"].AsString.Should().Be(quote.Applicant.LastName);
        document["Applicant"]["DateOfBirth"].ToUniversalTime().Should()
            .Be(quote.Applicant.DateOfBirth.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc));
        document["Applicant"]["EyeColor"].AsInt32.Should().Be((int)quote.Applicant.EyeColor);
        document["Applicant"]["Height"].AsInt32.Should().Be(quote.Applicant.Height);
        document["Applicant"]["Nationality"].AsString.Should().Be(quote.Applicant.Nationality);
        document["From"].ToUniversalTime().Should().Be(quote.From.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc));
        document["Duration"].AsInt32.Should().Be(quote.Duration);
        document["CreditScore"].AsInt32.Should().Be(creditScore);
        if (scoreCardResult.HasValue)
        {
            document["ScoreCardResult"].AsInt32.Should().Be((int)scoreCardResult.Value);
        }
        else
        {
            document["ScoreCardResult"].IsBsonNull.Should().BeTrue();
        }
    }
}