using Application.Common.Behaviours;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.Application.Common.Behaviours;

public class LoggingBehaviourTest
{
    private readonly LoggingBehaviour<TestRequest> _sut;

    public LoggingBehaviourTest()
    {
        var loggerMock = new Mock<ILogger<TestRequest>>();
        _sut = new LoggingBehaviour<TestRequest>(loggerMock.Object);
    }
    
    [Fact]
    public void Process_CallsLogger_WhenExecuted()
    {
        var testRequest = new TestRequest();
        var task = _sut.Process(testRequest, CancellationToken.None);

        task.Wait();

        task.Status.Should().Be(TaskStatus.RanToCompletion);
    }

    public class TestRequest
    {
        
    }
}