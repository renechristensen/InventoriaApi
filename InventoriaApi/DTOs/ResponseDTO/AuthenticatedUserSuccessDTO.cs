namespace InventoriaApi.DTOs.ResponseDTO;
public class AuthenticatedUserSuccessDTO
{
    public int UserID { get; set; }
    public string Displayname { get; set; }
    public string StudieEmail { get; set; }
    public int CompanyID { get; set; }
}
