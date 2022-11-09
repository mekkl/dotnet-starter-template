using FluentAssertions;
using TechTalk.SpecFlow;

namespace Tests.BDD.Steps;

[Binding]
public sealed class AdditionStepDefinitions
{
    private readonly ScenarioContext _scenarioContext;
    private int Num1 { get; set; }
    private int Num2 { get; set; }

    public AdditionStepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [When("i add (.*) and (.*)")]
    public void WhenIAddAnd(int number1, int number2)
    {
        Num1 = number1;
        Num2 = number2;
    }

    [Then("the result should be (.*)")]
    public void ThenTheResultShouldBe(int result)
    {
        result.Should().Be(Num1 + Num2);
    }
}
