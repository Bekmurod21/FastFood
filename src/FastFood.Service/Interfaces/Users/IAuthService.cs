﻿using FastFood.Service.DTOs.LoginDto;

namespace FastFood.Service.Interfaces.Users
{
    public interface IAuthService
    {
        Task<LoginForResultDto> AuthenticateAsync(LoginForCreationDto dto);
    }
}
