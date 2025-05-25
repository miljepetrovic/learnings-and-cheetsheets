using EntityFramework.Api.Enums;

namespace EntityFramework.Api.Entities;

public class Board
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Status Status { get; set; } = Status.Active;
    public List<Column> Columns { get; set; } = [];
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public void Archive()  => Status = Status.Archived;
}
