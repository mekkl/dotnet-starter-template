using Domain.Common;

namespace Domain.Model;

public class Member : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}