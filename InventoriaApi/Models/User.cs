namespace InventoriaApi.Models;
public class User
{
    public int UserID { get; set; }
    public string Displayname { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string StudieEmail { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastLoginDate { get; set; }
    public int CompanyID { get; set; }

    // Navigation properties
    public Company Company { get; set; }
    public List<UserRole> UserRoles { get; set; }
}

