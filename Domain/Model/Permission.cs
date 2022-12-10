using Domain.Common;

namespace Domain.Model;

public class Permission : BaseEntity
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}