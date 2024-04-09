using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace InventoriaApi.Services.Repositories
{
    public class ReservationRepository: GenericRepository<Reservation, InventoriaDBcontext>, IReservationRepository
    {
        public ReservationRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }
    }
}