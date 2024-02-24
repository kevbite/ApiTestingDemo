using ApiTestingDemo.InsuranceApi.Bureau;

namespace ApiTestingDemo.InsuranceApi.Quotation;

public static class QuotesEndpoints
{
    public static RouteHandlerBuilder MapQuotes(this IEndpointRouteBuilder builder)
    {
        return builder.MapPost("/quotes", async (QuoteResourceRepresentation resourceRepresentation,
                TimeProvider timeProvider,
                IBureauClient bureauClient) =>
            {
                var today = DateOnly.FromDateTime(timeProvider.GetUtcNow().DateTime);

                if (!Quote.TryCreate(today,
                        resourceRepresentation.Applicant.ToApplicant(),
                        resourceRepresentation.From,
                        resourceRepresentation.Duration,
                        out var quote,
                        out var errorMessage))
                {
                    return Results.BadRequest(errorMessage);
                }
                
                var bureauData = await bureauClient.GetBureauAsync(
                    quote.Applicant.FirstName,
                    quote.Applicant.LastName,
                    quote.Applicant.DateOfBirth);

                if (bureauData.Success)
                {
                    quote.CalculateQuotation(today,
                        bureauData.Data?.CreditScore ?? throw new InvalidOperationException("bureau data is null") );
                }

                return Results.Ok(QuoteResourceRepresentation.FromQuote(quote));
            })
            .WithName("PostQuote")
            .WithOpenApi();
    }
}