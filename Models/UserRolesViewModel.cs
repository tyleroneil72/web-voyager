namespace web_voyager.Models;

public class UserRolesViewModel
{
    public required string UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required IEnumerable<string> Roles { get; set; }
}