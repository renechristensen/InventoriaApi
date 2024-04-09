using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using InventoriaApi.Data;
using InventoriaApi.Services.Repositories;
using InventoriaApi.Services.RepositoryInterfaces;

var builder = WebApplication.CreateBuilder(args);

// I got some inspiration from https://medium.com/@vndpal/how-to-implement-jwt-token-authentication-in-net-core-6-ab7f48470f5c
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
//var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });



// Add services to the container.
// add repositories
builder.Services.AddScoped<IAlertRepository, AlertRepository>();
builder.Services.AddScoped<IAlertTypeRepository, AlertTypeRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IDataCenterRepository, DataCenterRepository>();
builder.Services.AddScoped<IDataRackRepository, DataRackRepository>();
builder.Services.AddScoped<IDataRacksChangeLogRepository, DataRacksChangeLogRepository>();
builder.Services.AddScoped<IEnvironmentalReadingRepository, EnvironmentalReadingRepository>();
builder.Services.AddScoped<IEnvironmentalSettingRepository, EnvironmentalSettingRepository>();
builder.Services.AddScoped<IEquipmentRackUnitRepository, EquipmentRackUnitRepository>();
builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<IRackAccessPermissionRepository, RackAccessPermissionRepository>();
builder.Services.AddScoped<IRackUnitRepository, RackUnitRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservedRackUnitRepository, ReservedRackUnitRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IServerRoomRepository, ServerRoomRepository>();
builder.Services.AddScoped<IUserChangeLogRepository, UserChangeLogRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();


// adding controllers
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext for MySQL
builder.Services.AddDbContext<InventoriaDBcontext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();