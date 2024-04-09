using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace InventoriaApi.Services.Repositories
{
    public class DataCenterRepository: GenericRepository<DataCenter, InventoriaDBcontext>, IDataCenterRepository
    {
        public DataCenterRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }
    }
}