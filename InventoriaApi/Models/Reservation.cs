namespace InventoriaApi.Models;

public class Reservation
{
    public int ReservationID { get; set; }
    public int UserID { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Background { get; set; }

    // Navigation property
    public User User { get; set; }
    public List<ReservedRackUnit> ReservedRackUnits { get; set; }
}

