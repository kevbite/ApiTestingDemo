using ApiTestingDemo.FunctionalTests.Bureau;
using ApiTestingDemo.InsuranceApi.Bureau;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace ApiTestingDemo.FunctionalTests;

public class InsuranceApiApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddSingleton<MockBureauClient>();
            services.AddSingleton<IBureauClient>(provider => provider.GetRequiredService<MockBureauClient>());
        });
    }
}