using FluentAssertions;
using Shared.Extensions;

namespace Tests.Shared.Extensions;

public class JsonExtensionTest
{
    [Fact]
    public void As_WithValidJson_ExpectObjectWithValues()
    {
        var id = Guid.NewGuid().ToString();
        const int value = 123;
        const double valueDouble = 123.123d;
        var description = Guid.NewGuid().ToString();
        const bool isActive = true;
        
        var jsonString = $"{{ \"Id\": \"{id}\", \"Value\": {value}, \"ValueDouble\": {valueDouble}, \"Description\": \"{description}\", \"IsActive\": {isActive.ToString().ToLower()} }}";
        
        var actual = jsonString.As<TestObject>();

        actual.Should().NotBeNull();
        actual!.Id.Should().Be(id);
        actual.Value.Should().Be(value);
        actual.ValueDouble.Should().Be(valueDouble);
        actual.Description.Should().Be(description);
        actual.IsActive.Should().Be(isActive);
    }
    
    [Fact]
    public void AsJson_WithObject_ExpectJson()
    {
        var testObject = new TestObject
        {
            Id = Guid.NewGuid(),
            Value = 123,
            ValueDouble = 123.123d,
            Description = Guid.NewGuid().ToString(),
            IsActive = true,
        };

        var actual = testObject.AsJson();

        actual.Should().NotBeNull();
        actual.Should().Contain($"\"Id\":\"{testObject.Id}\"");
        actual.Should().Contain($"\"Value\":{testObject.Value}");
        actual.Should().Contain($"\"ValueDouble\":{testObject.Value}");
        actual.Should().Contain($"\"Description\":\"{testObject.Description}\"");
        actual.Should().Contain($"\"IsActive\":{testObject.IsActive.ToString().ToLower()}");
    }

    private record TestObject
    {
        public Guid Id { get; init; }
        public int Value { get; init; }
        public double ValueDouble { get; init; }
        public string Description { get; init; } = string.Empty;
        public bool IsActive { get; init; }
    }
}