using System.Net;
using ApiTestingDemo.InsuranceApi;
using FluentAssertions;

namespace ApiTestingDemo.FunctionalTests.Quotation;

public class PostQuoteTests : IAsyncDisposable
{
    private readonly Harness _harness = new();

    [Fact]
    public async Task ShouldReturn400BadRequestForFromDateInPast()
    {
        var quote = QuoteResourceRepresentations.SuccessfulQuote
            with
            {
                From = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1))
            };

        var (statusCode, _) = await _harness.PostQuote(JsonNodeQuoteBuilder.Build(quote));

        statusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(366)]
    public async Task ShouldReturn400BadRequestForDurationOutOfRange(int duration)
    {
        var quote = QuoteResourceRepresentations.SuccessfulQuote
            with
            {
                Duration = duration
            };

        var (statusCode, _) = await _harness.PostQuote(JsonNodeQuoteBuilder.Build(quote));

        statusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturn200SuccessWithResponseBodyOfDeclinedForNoMatchingBureauData()
    {
        var quote = QuoteResourceRepresentations.SuccessfulQuote;

        var (statusCode, body) = await _harness.PostQuote(JsonNodeQuoteBuilder.Build(quote));

        statusCode.Should().Be(HttpStatusCode.OK);
        QuoteJsonDocumentAssertions.Assert(body!, quote, "Declined");
    }

    [Fact]
    public async Task ShouldStoreQuoteDocumentForNoMatchingBureauData()
    {
        var quote = QuoteResourceRepresentations.SuccessfulQuote;

        var (_, content) = await _harness.PostQuote(JsonNodeQuoteBuilder.Build(quote));

        var id = content!.RootElement.GetProperty("id").GetGuid();
        var document = _harness.Mongo.GetQuote(id);
        BsonDocumentQuoteAssertions.Assert(document, id, quote, 0);
    }
    
    [Fact]
    public async Task ShouldReturn200SuccessWithResponseBodyOfDeclinedForBadBureauData()
    {
        var quote = QuoteResourceRepresentations.SuccessfulQuote;
        _harness.Bureau.AddBadBureauData(quote.Applicant);

        var (statusCode, body) = await _harness.PostQuote(JsonNodeQuoteBuilder.Build(quote));

        statusCode.Should().Be(HttpStatusCode.OK);
        QuoteJsonDocumentAssertions.Assert(body!, quote, "Declined");
    }

    [Fact]
    public async Task ShouldReturn200SuccessWithResponseBodyOfReferredForReferredQuote()
    {
        var quote = QuoteResourceRepresentations.ReferredQuote;
        _harness.Bureau.AddGoodBureauData(quote.Applicant);

        var (statusCode, body) = await _harness.PostQuote(JsonNodeQuoteBuilder.Build(quote));

        statusCode.Should().Be(HttpStatusCode.OK);
        QuoteJsonDocumentAssertions.Assert(body!, quote, "Referred");
    }

    [Fact]
    public async Task ShouldReturn200SuccessWithResponseBodyOfReferredForDeclinedQuote()
    {
        var quote = QuoteResourceRepresentations.DeclinedQuote;
        _harness.Bureau.AddGoodBureauData(quote.Applicant);

        var (statusCode, body) = await _harness.PostQuote(JsonNodeQuoteBuilder.Build(quote));

        statusCode.Should().Be(HttpStatusCode.OK);
        QuoteJsonDocumentAssertions.Assert(body!, quote, "Declined");
    }

    [Fact]
    public async Task ShouldReturn200SuccessWithResponseBodyOfApprovedForSuccessfulQuote()
    {
        var quote = QuoteResourceRepresentations.SuccessfulQuote;
        _harness.Bureau.AddGoodBureauData(quote.Applicant);

        var (statusCode, body) = await _harness.PostQuote(JsonNodeQuoteBuilder.Build(quote));

        statusCode.Should().Be(HttpStatusCode.OK);
        QuoteJsonDocumentAssertions.Assert(body!, quote, "Approved");
    }

    [Fact]
    public async Task ShouldStoreQuoteDocumentForSuccessfulQuote()
    {
        var quote = QuoteResourceRepresentations.SuccessfulQuote;
        var (_, creditScore) = _harness.Bureau.AddGoodBureauData(quote.Applicant);

        var (_, content) = await _harness.PostQuote(JsonNodeQuoteBuilder.Build(quote));
      
        var id = content!.RootElement.GetProperty("id").GetGuid();
        var document = _harness.Mongo.GetQuote(id);
        BsonDocumentQuoteAssertions.Assert(document, id, quote, creditScore, ScoreCardResult.Approved);
    }
    
    public async ValueTask DisposeAsync()
    {
        await _harness.DisposeAsync();
    }
}