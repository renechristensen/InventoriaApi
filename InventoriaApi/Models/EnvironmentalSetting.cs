using InventoriaApi.Models;

public class EnvironmentalSetting
{
    public int EnvironmentalSettingsID { get; set; }
    public int ServerRoomID { get; set; }
    public float TemperatureUpperLimit { get; set; }
    public float TemperatureLowerLimit { get; set; }
    public float HumidityUpperLimit { get; set; }
    public float HumidityLowerLimit { get; set; }
    public DateTime LatestChange { get; set; }

    // Navigation property
    public ServerRoom ServerRoom { get; set; }
}
