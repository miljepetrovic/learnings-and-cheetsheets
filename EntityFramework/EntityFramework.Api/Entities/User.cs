namespace EntityFramework.Api.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public List<Board> Boards { get; set; } = [];
}