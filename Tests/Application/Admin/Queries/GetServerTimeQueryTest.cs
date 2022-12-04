using Application.Admin.Queries;
using FluentAssertions;

namespace Tests.Application.Admin.Queries;

public class GetServerTimeQueryTest
{
    [Fact]
     public async Task Test()
     {
         var query = new GetServerTimeQueryHandler();
         var input = new GetServerTimeQuery();

         var actual = await query.Handle(input, CancellationToken.None);

         actual.Should().BeCloseTo(DateTimeOffset.Now, TimeSpan.FromSeconds(2));
     }
}