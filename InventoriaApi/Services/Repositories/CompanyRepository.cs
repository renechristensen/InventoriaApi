using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace InventoriaApi.Services.Repositories
{
    public class CompanyRepository : GenericRepository<Company, InventoriaDBcontext>, ICompanyRepository
    {
        public CompanyRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }

        public async Task<IEnumerable<Company>> ReadAllRecordsWithDetailsAsync()
        {
            return await _context.Companies
                .Include(c => c.DataCenters)
                .Include(c => c.Users)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
