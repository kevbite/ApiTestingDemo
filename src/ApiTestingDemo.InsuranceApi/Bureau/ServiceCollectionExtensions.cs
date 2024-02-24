namespace ApiTestingDemo.InsuranceApi.Bureau;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBureau(this IServiceCollection services)
    {
        return services.AddSingleton<IBureauClient, BureauClient>();
    }
}