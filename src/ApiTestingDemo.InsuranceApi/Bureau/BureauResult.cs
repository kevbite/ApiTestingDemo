namespace ApiTestingDemo.InsuranceApi.Bureau;

public record BureauResult(
    bool Success,
    BureauData? Data
);