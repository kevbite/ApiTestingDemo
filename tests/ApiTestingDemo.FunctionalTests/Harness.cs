using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using ApiTestingDemo.FunctionalTests.Bureau;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ApiTestingDemo.FunctionalTests;

public class Harness : IAsyncDisposable, IHarness
{
    private readonly WebApplicationFactory<Program> _factory;

    public Harness()
    {
        _factory = new InsuranceApiApplicationFactory();
        Mongo = new MongoDbHarness(this);
        Bureau = new BureauHarness(this);
    }

    public IServiceProvider Services => _factory.Services;

    public MongoDbHarness Mongo { get; }
    public BureauHarness Bureau { get; }

    public async ValueTask DisposeAsync()
    {
        await _factory.DisposeAsync();
    }

    public async Task<(HttpStatusCode StatusCode, JsonDocument? content)> PostQuote(JsonNode jsonNode)
    {
        using var client = _factory.CreateClient();

        using var response = await client.PostAsync(
            "/quotes",
            new StringContent(
                jsonNode.ToString(),
                Encoding.UTF8,
                "application/json"));

        var content = response.Content.Headers.ContentLength > 0
            ? await response.Content.ReadFromJsonAsync<JsonDocument>()
            : null;

        return (response.StatusCode, content);
    }
}