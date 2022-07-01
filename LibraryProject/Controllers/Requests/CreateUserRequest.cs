using LibraryProject.Models;

namespace LibraryProject.Controllers.Requests;

public class CreateUsersRequest
{
    public long UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
}