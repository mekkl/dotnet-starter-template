using TechTalk.SpecFlow;
using System;

// Doc: https://docs.specflow.org/projects/specflow/en/latest/Bindings/Hooks.html?gclid=CjwKCAiA_omPBhBBEiwAcg7smZ5ZP-4ZUqEUZ-u0UVyE4O94q8PnNufYUS6ky6aG2wZ4lz3h7q31txoC3XgQAvD_BwE
namespace Tests.BDD.Hooks;

[Binding]
public sealed class AddHook
{
   private ScenarioContext _scenarioContext;

    public AddHook(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeScenario(Order = 1)]
    public void BeforeScenarioFirst()
    {
        Console.WriteLine($"Starting before scenario first {_scenarioContext.ScenarioInfo.Title}");
    }

    [BeforeScenario(Order = 100)]
    public void BeforeScenarioSecond()
    {
        Console.WriteLine($"Starting before scenario second {_scenarioContext.ScenarioInfo.Title}");
    }

    [BeforeFeature]
    public static void SetupStuffForFeatures(FeatureContext featureContext)
    {
        Console.WriteLine($"Starting before feature {featureContext.FeatureInfo.Title}");
    }

    [BeforeTestRun]
    public static void BeforeTestRunInjection(ITestRunnerManager testRunnerManager, ITestRunner testRunner)
    {
        Console.WriteLine("Starting before test run");
        //All parameters are resolved from the test thread container automatically.
        //Since the global container is the base container of the test thread container, globally registered services can be also injected.

        //ITestRunManager from global container
        var location = testRunnerManager.TestAssembly.Location;
        
        //ITestRunner from test thread container
        var threadId = testRunner.ThreadId;
    }
}
