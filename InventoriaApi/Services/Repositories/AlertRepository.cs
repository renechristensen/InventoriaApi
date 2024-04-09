using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace InventoriaApi.Services.Repositories
{
    public class AlertRepository: GenericRepository<Alert, InventoriaDBcontext>, IAlertRepository
    {
        public AlertRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }
    }
}