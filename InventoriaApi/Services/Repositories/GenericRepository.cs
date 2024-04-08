using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using InventoriaApi.Data;

namespace InventoriaApi.Services.Repositories
{
    public class GenericRepository<T, DBContext> : IGenericRepository<T>
        where T : class
        where DBContext : InventoriaDBcontext
    {
        protected InventoriaDBcontext _context;
        public GenericRepository(InventoriaDBcontext DBcontext) { 
            this._context = DBcontext;
        }
        public async Task CreateRecord(T obj)
        {
            _context.Add(obj);
            await SaveRecord();
        }

        public async Task DeleteRecord(int id)
        {
            var record = await ReadRecordByID(id);
            if (record != null)
            {
                _context.Remove(record);
                await SaveRecord();
            }
            else
            {
                // no record found, do nothing
            }
        }

        public async Task<ICollection<T>> ReadAllRecords()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> ReadRecordByID(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public  async Task<bool> ReadRecordToVerify(int id)
        {
            var record = await ReadRecordByID(id);
            return record != null;
        }

        public async Task SaveRecord()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRecord(T obj)
        {
            _context.Update(obj);
            await SaveRecord();
        }
    }
}
