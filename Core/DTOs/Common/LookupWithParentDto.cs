namespace Core.DTOs.Common;

/// <summary>
/// Parent ilişkisi olan lookup entity'ler için DTO (Model, BodyType, District)
/// </summary>
public class LookupWithParentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid ParentId { get; set; }
    public string ParentName { get; set; } = string.Empty;
}
