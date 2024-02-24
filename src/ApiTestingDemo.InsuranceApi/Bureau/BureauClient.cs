namespace ApiTestingDemo.InsuranceApi.Bureau;

public class BureauClient : IBureauClient
{
    public Task<BureauResult> GetBureauAsync(string firstName, string lastName, DateOnly dateOfBirth)
    {
        // TODO: Implement the actual API call
        throw new NotImplementedException();
    }
}