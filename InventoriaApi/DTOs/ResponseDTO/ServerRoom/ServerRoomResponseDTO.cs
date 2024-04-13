using System;

namespace InventoriaApi.DTOs.ResponseDTO
{
    public class ServerRoomResponseDTO
    {
        public int ServerRoomID { get; set; }
        public string ServerRoomName { get; set; }
        public int RackCapacity { get; set; }
        public DateTime StartupDate { get; set; }
        public int DataCenterID { get; set; }
        public string DataCenterName { get; set; }
    }
}
