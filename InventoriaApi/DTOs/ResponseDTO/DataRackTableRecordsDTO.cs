namespace InventoriaApi.DTOs.ResponseDTO
{
    public class DataRackTableRecordsDTO
    {
        public int dataRackID { get; set; }
        public string companyName { get; set; }
        public string dataCenterName { get; set; }
        public string serverRoomName { get; set; }
        public DateTime RackStartupDate { get; set; }
        public string RackStatus { get; set; }
        public int totalUnits { get; set; }
        public int availableUnits { get; set; }
    }
}
