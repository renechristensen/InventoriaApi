namespace InventoriaApi.Services.RepositoryInterfaces;
public interface IGenericRepository<T> where T : class
{
    // Using CRUD
    // Create functionalities
    Task CreateRecord(T obj);

    // Read functionalities
    Task<T> ReadRecordByID(int id);
    Task<bool> ReadRecordToVerify(int id);
    Task<ICollection<T>> ReadAllRecords();

    // Update functionalities
    Task UpdateRecord(T obj);
    // Delete Functionalities
    Task DeleteRecord(int id);

    // Not a CRUD functionality
    Task SaveRecord();
}
