using Domain.Common;
using Domain.Model;

namespace Domain.Auth;

public sealed class Role : Enumeration<Role>
{
    public static readonly Role Admin = new(1, "Admin");
    public static readonly Role Registered = new(2, "Registered");

    private Role(int id, string name) : base(id, name)
    {
    }

    public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    public ICollection<Member> Members { get; set; } = new List<Member>();
}