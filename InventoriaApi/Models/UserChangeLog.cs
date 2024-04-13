namespace InventoriaApi.Models;

public class UserChangeLog
{
    public int UserChangeLogID { get; set; }
    public int UserID { get; set; }
    public int ChangedByUserID { get; set; }
    public string ChangeType { get; set; }
    public DateTime ChangeTimestamp { get; set; }
    public string ChangeDescription { get; set; }

    // Navigation properties
    public User User { get; set; }
    public User ChangedByUser { get; set; }
}