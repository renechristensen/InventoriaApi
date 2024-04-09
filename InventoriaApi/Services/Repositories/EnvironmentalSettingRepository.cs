﻿using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace InventoriaApi.Services.Repositories
{
    public class EnvironmentalSettingRepository: GenericRepository<EnvironmentalSetting, InventoriaDBcontext>, IEnvironmentalSettingRepository
    {
        public EnvironmentalSettingRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }
    }
}