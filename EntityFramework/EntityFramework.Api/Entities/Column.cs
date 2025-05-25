using EntityFramework.Api.Enums;

namespace EntityFramework.Api.Entities;

public class Column
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Status Status { get; set; } = Status.Active;
    public List<Card> Cards { get; set; } = [];
    public Guid BoardId { get; set; }
    public Board Board { get; set; }
    
    public void Archive() => Status = Status.Archived;
}