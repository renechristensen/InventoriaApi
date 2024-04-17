using System.ComponentModel.DataAnnotations;

namespace InventoriaApi.DTOs.ResponseDTO;
public class DataRackTableRecordsDTO
{
    public string DatarackName { get; set; }
    public int DataRackID { get; set; }
    public string ServerRoomName { get; set; }
    public DateTime RackStartupDate { get; set; }
    public string RackStatus { get; set; }
    public int TotalUnits { get; set; }
    public int AvailableUnits { get; set; }
    public string DataCenterName { get; set; }
    public string CompanyName { get; set; }
    public string RackPlacement { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}
