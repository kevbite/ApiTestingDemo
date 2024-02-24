using Swashbuckle.AspNetCore.Annotations;

namespace ApiTestingDemo.InsuranceApi.Quotation;

public record QuoteResourceRepresentation(
    [property: SwaggerSchema(ReadOnly = true)] Guid? Id,
    ResourceRepresentationApplicant Applicant,
    DateOnly From,
    int Duration,
    [property: SwaggerSchema(ReadOnly = true)] QuoteResult? Result)
{
    public static QuoteResourceRepresentation FromQuote(Quote quote)
    {
        return new(
            quote.Id,
            ResourceRepresentationApplicant.FromApplicant(quote.Applicant),
            quote.From,
            quote.Duration,
            quote.Result);
    }
}