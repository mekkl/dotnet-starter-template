[![.NET](https://github.com/mekkl/mekkl.io/actions/workflows/wf-dotnet-build.yml/badge.svg)](https://github.com/mekkl/mekkl.io/actions/workflows/wf-dotnet-build.yml)
[![codecov](https://codecov.io/gh/pitwallsim/pitwallsim-server/branch/main/graph/badge.svg?token=s2XWnEQoWG)](https://codecov.io/gh/pitwallsim/pitwallsim-server)

# Introduction 
PitWallSim backend project

# Getting Started

## Setting up database for development
This solution is utilizing Docker container with a SQL server image when running the server locally.

First install Docker Desktop from https://www.docker.com/products/docker-desktop/

When installed open the commandline and execute the following to install the latest SQL server image:

``` 
docker pull mcr.microsoft.com/mssql/server:2022-latest
```

To start the container, use the following docker command:

```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

> Afterwards the container can be started/stopped via. the "play" botton in the `Containers` section via. Docker Desktop. 

![alt text](.github/readme/docker-desktop-start-container.png)


Ensure that this setting is found in the `appsettings.Local.json` file.

``` json
{
      "Azure": {
            "SqlConnectionString": "Data Source=localhost,1433; Persist Security Info=True;User ID=SA;Password=yourStrong(!)Password; TrustServerCertificate=True"
      }
}
```

## Start the app
First download and install the .NET Core SDK [here](https://dotnet.microsoft.com/download).

> You can check to see if you have installed the .Net Core SDK version (currently using the latest 6.0) with this command ```$ dotnet --version```

To startup the API open up a console, at the project root. 

Firstly go to the ```Presentation.WebApi``` folder:
```
$ cd Presentation.WebApi
```

Then start the API application with the following command:
```
$ dotnet run
```


The output from the command should display which url and port the API is listining on:
```
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\Users\ML\Desktop\portfolio-web\web-api
```

The API is documented with swagger. Just open the browser and type in the displayed url from the above output.

## Build and Test
Command for building the project (run the command at the project root):
```
$ dotnet build
```

Command for running all tests (run the command at the project root):
```
$ dotnet test
```

Command for running all tests, and generating coverage data. The result is ending op in each test project in the dir ```Tests/TestResults/<GUID>/coverage.cobertura.xml```
```
$ dotnet test --collect:"XPlat Code Coverage" --settings Tests/coverlet.runsettings
```

Command for generating coverage report from results:
```
$ reportgenerator "-reports:*/TestResults/**/coverage.info" "-targetdir:Tests/CoverageReports" -reporttypes:html
```
> :warning: Usage of **reportgenerator** see: https://github.com/danielpalme/ReportGenerator

# Entity Framework

Link: https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=vs

`Microsoft.EntityFrameworkCore.Design` This package is required for the Entity Framework Core Tools to work. Ensure your startup project is correct
`Microsoft.EntityFrameworkCore.Tools` nuget enables entity framework commands!

> :warning: When using the Package Manager Console, set fefault Project to `Infrastructure`

## Add migration 
```
> add-migration <migration-name> -OutputDir Your\Directory 
```

``` 
> dotnet ef migrations add <migration-name> -OutputDir --output-dir Your/Directory
```

## Apply migration
```
> update-database
```

``` 
> dotnet ef database update 
```

## Apply specific migration (rollback to previous)
```
> Update-Database <previous-migration-name>
```

```
> dotnet ef database update <previous-migration-name>
```

## Remove migration (latest migration)
```
> Remove-Migration
```

```
> dotnet ef migrations remove
```

## List migrations
```
> Get-Migration
```

```
> dotnet ef migrations list
```

# Nuget versions update
Command for checking nuget version
```
$ dotnet outdated
```

Command for checking nuget version and upgrade
```
$ dotnet outdated -u
```

> :warning: To use **dotnet outdated** the tool must first be installed. See: https://github.com/dotnet-outdated/dotnet-outdated

# Benchmark testing
See: https://github.com/dotnet/BenchmarkDotNet

# Load testing
See: https://www.youtube.com/watch?v=r-Jte8Y8zag

# Versioning
See: https://medium.com/fiverr-engineering/major-minor-patch-a5298e2e1798

# API Versioning
See: https://www.youtube.com/watch?v=iVHtKG0eU_s
See: https://dev.to/htissink/asp-net-core-api-path-versioning-197o

# JSON Serialize
See: https://github.com/neuecc/Utf8Json

# SignalR
See: https://docs.microsoft.com/en-us/aspnet/core/signalr/streaming?view=aspnetcore-6.0

# Test resources
- https://dejanstojanovic.net/aspnet/2020/may/setting-up-code-coverage-reports-in-azure-devops-pipeline/
- https://medium.com/@tarik.nzl/publishing-test-coverage-with-net-core-and-vsts-build-pipelines-39a2f29dfa12
- https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=windows
- Install ReportGenerator for DevOps for usage in pipeline: https://www.jamescroft.co.uk/combining-multiple-code-coverage-results-in-azure-devops/
- https://github.com/coverlet-coverage/coverlet/blob/master/Documentation/VSTestIntegration.md
- https://github.com/coverlet-coverage/coverlet
- https://coveralls.io/
- https://docs.specflow.org/projects/specflow/en/latest/vscode/test-execution.html

- Maybe see for multiple test projects: https://jpenniman.blogspot.com/2019/10/code-coverage-for-multiple-projects-in.html

# Enforce HTTPS
- https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-6.0&tabs=visual-studio

# E2E (End-to-end) testing
- https://playwright.dev/dotnet/

# Fluent Validation Doc
- https://docs.fluentvalidation.net/en/latest/inheritance.html

# Codecov (coverage report)
- https://github.com/codecov/codecov-action


