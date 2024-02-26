using ApiTestingDemo.InsuranceApi.Quotation;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ApiTestingDemo.FunctionalTests;

public class MongoDbHarness(IHarness harness)
{
    public BsonDocument GetQuote(Guid id)
    {
        using var scope = harness.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
        var collection = db.GetCollection<BsonDocument>("quotes");
        
        return collection.Find(Builders<BsonDocument>.Filter.Eq("_id", new BsonBinaryData(id, GuidRepresentation.Standard)))
            .FirstOrDefault();
    }
}