using MediatR;

namespace Application.Admin.Queries;

public record GetServerTimeQuery : IRequest<DateTimeOffset>;

public class GetServerTimeQueryHandler : IRequestHandler<GetServerTimeQuery, DateTimeOffset>
{
    public Task<DateTimeOffset> Handle(GetServerTimeQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(DateTimeOffset.Now);
    }
}