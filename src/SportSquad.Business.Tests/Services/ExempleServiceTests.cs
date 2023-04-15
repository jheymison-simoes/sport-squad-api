using SportSquad.Business.Tests.Fixture;
using FluentAssertions;
using Xunit;

namespace SportSquad.Business.Tests.Services;

[Collection(nameof(ExempleCollection))]
public class ExempleServiceTests
{
    private readonly ExempleFixture _exempleFixture;

    public ExempleServiceTests(ExempleFixture exempleFixture)
    {
        _exempleFixture = exempleFixture;
    }
    
    [Fact(DisplayName = "Testar testes unitários")]
    [Trait("Category", "Exemple Service")]
    public async void Exemple_Test_UnitTestes()
    {
        _exempleFixture.GenerateExempleService();
        // var result = await _exempleFixture.ExempleService.GetString();
        // result.Should().Be("Exemple Service Success");
    }
}