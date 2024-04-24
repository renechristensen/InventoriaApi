using System;

namespace InventoriaApi.DTOs.ResponseDTO
{
    public class AlertResponseDTO
    {
        public int AlertID { get; set; }
        public int AlertTypeID { get; set; }
        public string AlertTypeName { get; set; } 
        public string AlertTypeDescription { get; set; }
        public bool ThresholdExceeded { get; set; }
        public int EnvironmentalReadingID { get; set; }
        public float Temperature { get; set; }  
        public float Humidity { get; set; } 
        public DateTime ReadingTimestamp { get; set; }
        public DateTime AlertTimestamp { get; set; }
        public bool Resolved { get; set; }

        // EnvironmentalSetting details
        public int EnvironmentalSettingsID { get; set; }
        public float TemperatureUpperLimit { get; set; }
        public float TemperatureLowerLimit { get; set; }
        public float HumidityUpperLimit { get; set; }
        public float HumidityLowerLimit { get; set; }
        public DateTime LatestChange { get; set; }

        // ServerRoom details
        public int ServerRoomID { get; set; }
        public string ServerRoomName { get; set; }
        public int RackCapacity { get; set; }
        public DateTime StartupDate { get; set; }
    }
}
