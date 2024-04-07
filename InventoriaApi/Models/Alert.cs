namespace InventoriaApi.Models;

public class Alert
{
    public int AlertID { get; set; }
    public int AlertTypeID { get; set; }
    public bool ThresholdExceeded { get; set; }
    public int EnvironmentalReadingID { get; set; }
    public DateTime AlertTimestamp { get; set; }
    public bool Resolved { get; set; }

    // Navigation properties
    public AlertType AlertType { get; set; }
    public EnvironmentalReading EnvironmentalReading { get; set; }
}

