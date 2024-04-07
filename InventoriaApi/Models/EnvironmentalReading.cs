namespace InventoriaApi.Models;

public class EnvironmentalReading
{
    public int EnvironmentalReadingID { get; set; }
    public float Temperature { get; set; }
    public float Humidity { get; set; }
    public DateTime ReadingTimestamp { get; set; }
}
