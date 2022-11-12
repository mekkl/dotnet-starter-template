# TestContainer Container alternatives

## Docker container postgres
```csharp
private readonly TestcontainersContainer _dbContainer = new TestcontainersBuilder<TestcontainersContainer>()
    .WithImage("postgres:latest")
    .WithEnvironment("POSTGRES_USER", "nick")
    .WithEnvironment("POSTGRES_PASSWORD", "cha")
    .WithEnvironment("POSTGRES_DB", "mydb")
    .WithPortBinding(5555, 5432)
    .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
    .Build();
```

## Docker container mssql
```csharp
private readonly TestcontainersContainer _dbContainer = new TestcontainersBuilder<TestcontainersContainer>()
    .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
    .WithEnvironment("ACCEPT_EULA", "Y")
    .WithEnvironment("MSSQL_SA_PASSWORD", "yourStrong(!)Password")
    .WithEnvironment("POSTGRES_DB", "mydb")
    .WithPortBinding(5555, 1433)
    .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
    .Build();
```

## Postgres container
```csharp
private readonly PostgreSqlTestcontainer _dbContainer = new TestcontainersBuilder<PostgreSqlTestcontainer>()
    .WithDatabase(new PostgreSqlTestcontainerConfiguration()
    {
        Database = "testDb",
        Username = "testUsn",
        Password = "testPws"
    })
    .WithImage("postgres:latest")
    .Build();
```