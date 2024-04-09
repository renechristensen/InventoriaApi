namespace InventoriaApi.Models;
public class Company
{
    public int CompanyID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Navigation properties
    public List<DataCenter> DataCenters { get; set; }
    public List<User> Users { get; set; }
}
