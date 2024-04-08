﻿using System.ComponentModel.DataAnnotations;

namespace InventoriaApi.DTOs;
public class AuthenticateUserDTO
{
    [Required, DataType(DataType.Password)]
    public string PasswordHash { get; set; }

    [Required, EmailAddress]
    public string StudieEmail { get; set; }
}