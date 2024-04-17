namespace InventoriaApi.DTOs.ResponseDTO
{
    public class RackUnitDTO
    {
        public int RackUnitID { get; set; }
        public int DataRackID { get; set; }
        public int UnitNumber { get; set; }
    }

    public class RackUnitFlatDTO
    {
        public int RackUnitID { get; set; }
        public int UnitNumber { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ReservationBackground { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentModel { get; set; }
        public string EquipmentType { get; set; }
    }
}