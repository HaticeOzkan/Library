namespace LibraryProject.Models;

public class User
{
    public Guid Id { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
}