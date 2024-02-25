using System.Text.Json;
using ApiTestingDemo.InsuranceApi.Quotation;
using FluentAssertions;

namespace ApiTestingDemo.FunctionalTests.Quotation;

public static class QuoteJsonDocumentAssertions
{
    public static void Assert(JsonDocument document, QuoteResourceRepresentation representation, string result)
    {
        var root = document!.RootElement;
        
        root.GetProperty("id").GetGuid().Should().NotBeEmpty();
        root.GetProperty("from").GetDateTime().Should().Be(representation.From.ToDateTime(TimeOnly.MinValue));
        root.GetProperty("duration").GetInt32().Should().Be(representation.Duration);
        
        var applicant = root.GetProperty("applicant");
        applicant.GetProperty("firstName").GetString().Should().Be(representation.Applicant.FirstName);
        applicant.GetProperty("lastName").GetString().Should().Be(representation.Applicant.LastName);
        applicant.GetProperty("dateOfBirth").GetDateTime().Should().Be(representation.Applicant.DateOfBirth.ToDateTime(TimeOnly.MinValue));
        applicant.GetProperty("eyeColor").GetString().Should().Be(representation.Applicant.EyeColor.ToString());
        applicant.GetProperty("height").GetDouble().Should().Be(representation.Applicant.Height);
        applicant.GetProperty("nationality").GetString().Should().Be(representation.Applicant.Nationality);
        
        root.GetProperty("result").GetString().Should().Be(result);
    }
}