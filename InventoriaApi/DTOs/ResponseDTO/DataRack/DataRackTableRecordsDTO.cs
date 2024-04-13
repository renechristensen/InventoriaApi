using System.ComponentModel.DataAnnotations;

namespace InventoriaApi.DTOs.ResponseDTO;
public class DataRackTableRecordsDTO
{
    public int DataRackID { get; set; }

    [StringLength(255, ErrorMessage = "Server room name must be 255 characters or less")]
    public string ServerRoomName { get; set; }

    public DateTime RackStartupDate { get; set; }

    [StringLength(50, ErrorMessage = "Rack status must be 50 characters or less")]
    public string RackStatus { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Total units must be at least 1")]
    public int TotalUnits { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Available units cannot be negative")]
    public int AvailableUnits { get; set; }

    [StringLength(255, ErrorMessage = "Data center name must be 255 characters or less")]
    public string DataCenterName { get; set; }

    [StringLength(255, ErrorMessage = "Company name must be 255 characters or less")]
    public string CompanyName { get; set; }
}