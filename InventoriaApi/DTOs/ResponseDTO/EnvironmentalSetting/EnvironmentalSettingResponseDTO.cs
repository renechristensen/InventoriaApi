namespace InventoriaApi.DTOs.ResponseDTO
{
    public class EnvironmentalSettingDTO
    {
        public int EnvironmentalSettingID { get; set; }
        public int ServerRoomID { get; set; }
        public float TemperatureUpperLimit { get; set; }
        public float TemperatureLowerLimit { get; set; }
        public float HumidityUpperLimit { get; set; }
        public float HumidityLowerLimit { get; set; }
        public DateTime LatestChange { get; set; }
    }
}