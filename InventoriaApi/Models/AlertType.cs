namespace InventoriaApi.Models;

public class AlertType
{
    public int AlertTypeID { get; set; }
    public string TypeName { get; set; }
    public string Description { get; set; }

    // Navigation property
    public List<AlertType> Alerts { get; set; }
}
