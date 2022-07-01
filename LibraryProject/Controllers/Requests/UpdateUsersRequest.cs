namespace LibraryProject.Controllers.Requests;

public class UpdateUsersRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
}