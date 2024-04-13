namespace InventoriaApi.DTOs.ResponseDTO
{
    public class EnvironmentalReadingDTO
    {
        public int EnvironmentalReadingID { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public DateTime ReadingTimestamp { get; set; }
    }
}