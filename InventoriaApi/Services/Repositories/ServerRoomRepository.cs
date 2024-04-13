using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace InventoriaApi.Services.Repositories
{
    public class ServerRoomRepository: GenericRepository<ServerRoom, InventoriaDBcontext>, IServerRoomRepository
    {
        public ServerRoomRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }
        public async Task<IEnumerable<ServerRoom>> ReadAllRecordsWithDetailsAsync()
        {
            return await _context.ServerRooms
                .Include(sr => sr.DataCenter)
                .ThenInclude(dc => dc.Company)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}