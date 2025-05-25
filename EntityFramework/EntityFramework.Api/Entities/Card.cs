using EntityFramework.Api.Enums;

namespace EntityFramework.Api.Entities;

public class Card
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Status Status { get; set; } = Status.Active;
    public Guid ColumnId { get; set; }
    public Column Column { get; set; }
    
    public void Archive() => Status = Status.Archived;
}   