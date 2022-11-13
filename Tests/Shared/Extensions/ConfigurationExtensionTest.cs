using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Shared.Extensions;

namespace Tests.Shared.Extensions;

public class ConfigurationExtensionTest
{
    private readonly Mock<IConfiguration> _configurationMock;

    public ConfigurationExtensionTest()
    {
        _configurationMock = new Mock<IConfiguration>();
    }
    
    [Theory]
    [InlineData("1", 1)]
    [InlineData("-2", -2)]
    [InlineData("0", 0)]
    [InlineData("10000", 10000)]
    [InlineData(null, 0)]
    [InlineData("text", 0)]
    public void GetOrDefault_IntValue_ExpectValue(string value, int expected)
    {
        _configurationMock.Setup(mock => mock[It.IsAny<string>()])
            .Returns(value);

        var actual = _configurationMock.Object.GetOrDefault<int>(string.Empty);

        actual.Should().Be(expected);
    }
    
    [Theory]
    [InlineData("test")]
    [InlineData("")]
    [InlineData("123")]
    [InlineData("@£€€@5123sadf!")]
    public void GetOrDefault_StringValue_ExpectValue(string value)
    {
        _configurationMock.Setup(mock => mock[It.IsAny<string>()])
            .Returns(value);

        var actual = _configurationMock.Object.GetOrDefault<string>(string.Empty);

        actual.Should().Be(value);
    }
    
    [Theory]
    [InlineData("true", true)]
    [InlineData("false", false)]
    [InlineData("True", true)]
    [InlineData("False", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void GetOrDefault_BooleanValue_ExpectValue(string value, bool expected)
    {
        _configurationMock.Setup(mock => mock[It.IsAny<string>()])
            .Returns(value);

        var actual = _configurationMock.Object.GetOrDefault<bool>(string.Empty);

        actual.Should().Be(expected);
    }
}
