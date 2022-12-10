using Domain.Common;

namespace Domain.Model;

public class Member : BaseIdentityEntity
{
    public string Name { get; set; } = string.Empty;
    
    public ICollection<Role> Roles { get; set; } = new List<Role>();
}