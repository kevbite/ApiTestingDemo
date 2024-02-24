using System.Text.Json;
using System.Text.Json.Nodes;
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
public class JsonNodeQuoteBuilder
{
    public static JsonNode Build(QuoteResourceRepresentation quoteResourceRepresentation)
    {
        var jsonNode = new JsonObject
        {
            ["from"] = quoteResourceRepresentation.From.ToString("O"),
            ["duration"] = quoteResourceRepresentation.Duration,
            ["applicant"] = new JsonObject
            {
                ["firstName"] = quoteResourceRepresentation.Applicant.FirstName,
                ["lastName"] = quoteResourceRepresentation.Applicant.LastName,
                ["dateOfBirth"] = quoteResourceRepresentation.Applicant.DateOfBirth.ToString("O"),
                ["eyeColor"] = quoteResourceRepresentation.Applicant.EyeColor.ToString(),
                ["height"] = quoteResourceRepresentation.Applicant.Height,
                ["nationality"] = quoteResourceRepresentation.Applicant.Nationality,
            }
        };

        return jsonNode;
    }
}