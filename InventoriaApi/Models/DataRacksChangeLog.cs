﻿namespace InventoriaApi.Models;

public class DataRacksChangeLog
{
    public int DataRacksChangeLogID { get; set; }
    public int DataRackID { get; set; }
    public int ChangedByUserID { get; set; }
    public string ChangeType { get; set; }
    public DateTime ChangeTimestamp { get; set; }
    public string ChangeDescription { get; set; }

    // Navigation properties
    public DataRack DataRack { get; set; }
    public User ChangedByUser { get; set; }
}
