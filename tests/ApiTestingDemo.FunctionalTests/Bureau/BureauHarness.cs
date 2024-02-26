using ApiTestingDemo.InsuranceApi.Bureau;
using ApiTestingDemo.InsuranceApi.Quotation;
using Microsoft.Extensions.DependencyInjection;

namespace ApiTestingDemo.FunctionalTests.Bureau;

public class BureauHarness(IHarness harness)
{
    private BureauData GoodBureauData => new BureauData(Guid.NewGuid(), 700);
    private BureauData BadBureauData => new BureauData(Guid.NewGuid(), 100);

    public BureauData AddGoodBureauData(ResourceRepresentationApplicant applicant)
    {
        AddBureauData(applicant.FirstName, applicant.LastName, applicant.DateOfBirth, GoodBureauData);

        return GoodBureauData;
    }
    
    public void AddBadBureauData(ResourceRepresentationApplicant applicant)
    {
        AddBureauData(applicant.FirstName, applicant.LastName, applicant.DateOfBirth, BadBureauData);
    }

    private void AddBureauData(string firstName, string lastName, DateOnly dateOfBirth, BureauData data)
    {
        using var scope = harness.Services.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<MockBureauClient>();
        
        client.AddBureauData(firstName, lastName, dateOfBirth, data);
    }
}