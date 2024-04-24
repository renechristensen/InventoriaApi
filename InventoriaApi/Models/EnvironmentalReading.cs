namespace InventoriaApi.Models;

public class EnvironmentalReading
{
    public int EnvironmentalReadingID { get; set; }
    public float Temperature { get; set; }
    public float Humidity { get; set; }
    public DateTime ReadingTimestamp { get; set; }

    // Foreign Key for EnvironmentalSetting
    public int EnvironmentalSettingsID { get; set; }

    // Navigation property
    public EnvironmentalSetting EnvironmentalSetting { get; set; }
}
