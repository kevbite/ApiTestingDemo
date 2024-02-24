namespace ApiTestingDemo.FunctionalTests;

public class MongoDbHarness
{
    private readonly IHarness _harness;

    public MongoDbHarness(IHarness harness)
    {
        _harness = harness;
    }
}