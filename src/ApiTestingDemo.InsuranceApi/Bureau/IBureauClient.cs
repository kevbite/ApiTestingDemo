namespace ApiTestingDemo.InsuranceApi.Bureau;

public interface IBureauClient
{
    public Task<BureauResult> GetBureauAsync(string firstName, string lastName, DateOnly dateOfBirth);
}