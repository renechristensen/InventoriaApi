﻿using InventoriaApi.Models;
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


        public async Task<IEnumerable<DataRack>> ReadAllRecordsWithDetailsAsync()
        {
            return await _context.DataRacks
                .Include(dr => dr.ServerRoom)
                    .ThenInclude(sr => sr.DataCenter)
                        .ThenInclude(dc => dc.Company)
                .AsNoTracking() 
                .ToListAsync();
        }

    }
}