using System.Text.Json.Nodes;
using ApiTestingDemo.InsuranceApi.Quotation;

namespace ApiTestingDemo.FunctionalTests.Quotation;

public static class JsonNodeQuoteBuilder
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