using Application.Common.Interfaces.Persistence;
using Domain.Model;
using MediatR;

namespace Application.Auth.Queries;

public record GetPermissionsQuery(Guid ClientId) : IRequest<HashSet<string>>;

public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, HashSet<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetPermissionsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<HashSet<string>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        var member = await _unitOfWork.MemberRepository.GetAsync(request.ClientId.ToString());

        var roles = member?.Roles.Select(role => role) ?? new List<Role>();

        return roles
            .SelectMany(role => role.Permissions)
            .Select(r => r.Name)
            .ToHashSet();
    }
}
