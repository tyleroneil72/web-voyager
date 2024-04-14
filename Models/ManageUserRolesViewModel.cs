namespace web_voyager.Models;

public class ManageUserRolesViewModel
{
    public required string RolesId { get; set; }
    public required string RoleName { get; set; }
    public bool Selected { get; set; }
}