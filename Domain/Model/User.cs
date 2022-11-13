using Domain.Common;

namespace Domain.Model;

public record User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}