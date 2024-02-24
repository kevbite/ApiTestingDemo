using ApiTestingDemo.InsuranceApi.Bureau;

namespace ApiTestingDemo.FunctionalTests.Bureau;

public class MockBureauClient : IBureauClient
{
    private readonly IDictionary<(string firstName, string lastName, DateOnly dateOfBirth), BureauData>
        _bureauData = new Dictionary<(string firstName, string lastName, DateOnly dateOfBirth), BureauData>();

    public Task<BureauResult> GetBureauAsync(string firstName, string lastName, DateOnly dateOfBirth)
    {
        var key = (firstName, lastName, dateOfBirth);
        
        return Task.FromResult(
            _bureauData.TryGetValue(key, out var data)
                ? new BureauResult(true, data)
                : new BureauResult(false, null));
    }
    
    public void AddBureauData(string firstName, string lastName, DateOnly dateOfBirth, BureauData data)
    {
        var key = (firstName, lastName, dateOfBirth);
        _bureauData.Add(key, data);
    }
}