using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace InventoriaApi.Services.Repositories
{
    public class AlertTypeRepository: GenericRepository<AlertType, InventoriaDBcontext>, IAlertTypeRepository
    {
        public AlertTypeRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }
    }
}