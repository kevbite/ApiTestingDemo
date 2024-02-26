using ApiTestingDemo.InsuranceApi.Data;
using ApiTestingDemo.InsuranceApi.Quotation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace ApiTestingDemo.InsuranceApi;

public static class BsonSetup
{
    private static bool _ranOnce = false;
    private static readonly object _lock = new object();

    public static void Setup()
    {
        if (_ranOnce) return;
        lock (_lock)
        {
            if (_ranOnce) return;
            
            BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
            BsonSerializer.RegisterSerializer(new DateOnlySerializer());
            BsonClassMap.RegisterClassMap<Quote>(map =>
            {
                map.AutoMap();
                map.MapProperty(x => x.Id).SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
                map.MapProperty(x => x.Applicant);
                map.MapProperty(x => x.From);
                map.MapProperty(x => x.Duration);
                map.MapProperty(x => x.CreditScore);
                map.MapProperty(x => x.ScoreCardResult);
                map.MapProperty(x => x.Result);
            });

            BsonClassMap.RegisterClassMap<Applicant>(map => { map.AutoMap(); });
            
            _ranOnce = true;

        }
    }
}