namespace Ordering.Domain.Common;

public abstract class EntityBase
{
    public int Id { get; set; }
    public string CreateBy { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
    public string LastModifiedBy { get; set; } = string.Empty;
    public DateTime? LastModifiedDate { get; set; }
}
