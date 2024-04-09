using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace InventoriaApi.Services.Repositories
{
    public class CompanyRepository: GenericRepository<Company, InventoriaDBcontext>, ICompanyRepository
    {
        public CompanyRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }
    }
}