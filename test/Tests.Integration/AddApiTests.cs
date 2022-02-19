using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Tests.Integration;

[Trait("Category", "IntegrationTest")]
public class AddApiTests : IClassFixture<WebApplicationFactory>
{
    private readonly WebApplicationFactory _webApplicationFactory;
    
    public AddApiTests(WebApplicationFactory webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Theory]
    [InlineData(1, 2, 3)]
    public async Task WHEN_Add_is_called_with_valid_values_THEN_returns_the_sum(
        int a, int b, int expectedSum)
    {
        var client = _webApplicationFactory.CreateClient();
        var response = await client.GetAsync($"/api/math/add/{a}/{b}");
        var result = int.Parse(await response.Content.ReadAsStringAsync());
        result.Should().Be(expectedSum);
    }
}
