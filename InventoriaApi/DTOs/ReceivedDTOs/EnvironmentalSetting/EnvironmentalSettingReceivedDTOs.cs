using System.ComponentModel.DataAnnotations;

namespace InventoriaApi.DTOs.ReceivedDTOs
{
    public class CreateEnvironmentalSettingDTO
    {
        [Required]
        public int ServerRoomID { get; set; }
        [Required]
        public float TemperatureUpperLimit { get; set; }
        [Required]
        public float TemperatureLowerLimit { get; set; }
        [Required]
        public float HumidityUpperLimit { get; set; }
        [Required]
        public float HumidityLowerLimit { get; set; }
    }

    public class UpdateEnvironmentalSettingDTO
    {
        public float? TemperatureUpperLimit { get; set; }
        public float? TemperatureLowerLimit { get; set; }
        public float? HumidityUpperLimit { get; set; }
        public float? HumidityLowerLimit { get; set; }
    }
}
