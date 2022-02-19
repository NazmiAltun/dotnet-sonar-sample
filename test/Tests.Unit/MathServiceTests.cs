using FluentAssertions;
using Xunit;
using Dotnet.Sonar.Sample.Services;

namespace Tests.Unit;

[Trait("Category", "UnitTest")]
public class MathServiceTests
{
    [Theory]
    [InlineData(1, 2, 3)]
    public void Add_Should_ReturnSumOfTwoNumbers(
        int a, int b, int sum)
    {
        var mathService = new MathService();
        mathService.Add(a,b).Should().Be(sum);
    }
}
