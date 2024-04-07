namespace InventoriaApi.Models;

public class ReservedRackUnit
{
    public int ReservedRackUnitID { get; set; }
    public int ReservationID { get; set; }
    public int RackUnitID { get; set; }

    // Navigation properties
    public Reservation Reservation { get; set; }
    public RackUnit RackUnit { get; set; }
}

