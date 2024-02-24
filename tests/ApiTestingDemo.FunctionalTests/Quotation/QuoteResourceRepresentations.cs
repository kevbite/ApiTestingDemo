using ApiTestingDemo.InsuranceApi;
using ApiTestingDemo.InsuranceApi.Quotation;

namespace ApiTestingDemo.FunctionalTests.Quotation;

public static class QuoteResourceRepresentations
{
    public static QuoteResourceRepresentation SuccessfulQuote => new(
        Id: null,
        new ResourceRepresentationApplicant(
            "John",
            "Doe",
            new DateOnly(1980, 1, 1),
            EyeColor.Blue,
            180,
            "UK"),
        DateOnly.FromDateTime(DateTime.UtcNow.AddDays(30)),
        365,
        Result: null);

    public static QuoteResourceRepresentation ReferredQuote =>
        SuccessfulQuote with
        {
            Applicant = SuccessfulQuote.Applicant with { Nationality = "US" }
        };

    public static QuoteResourceRepresentation DeclinedQuote =>
        SuccessfulQuote with
        {
            Applicant = SuccessfulQuote.Applicant with
            {
                DateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-10))
            }
        };
}