namespace Domain.Common;

public abstract class BaseIdentityEntity : BaseEntity
{
    public Guid Id { get; set; }
}