using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace InventoriaApi.Services.Repositories
{
    public class DataRackRepository: GenericRepository<DataRack, InventoriaDBcontext>, IDataRackRepository
    {
        public DataRackRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }
    }
}