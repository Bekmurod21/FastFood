﻿using FastFood.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using FastFood.Service.DTOs.UserDto;
using FastFood.Domain.Configurations;
using FastFood.Service.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;

namespace FastFood.WebApi.Controllers.Users
{
    public class UsersController : RestfulSense
    {
        private readonly IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }
        [HttpPut]
        public async Task<IActionResult> ChangePasswordAsync(UserForChangePasswordDto dto) =>
            Ok(new Response()
            {
                Code = 200,
                Message = "Success",
                Data = await this.service.ChangePasswordAsync(dto)
            });
        [HttpPost]
        public async ValueTask<IActionResult> PostAsyn(UserForCreationDto dto) =>
            Ok(new Response
            {
                Code = 200,
                Message = "Seccess",
                Data = await service.AddAsync(dto)
            });
        [HttpPut("{id}")]
        public async ValueTask<IActionResult> PutAsync(long id, UserForUpdateDto dto) =>
            Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await service.ModifyAsync(id, dto)
            });
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async ValueTask<IActionResult> DeleteAsync(long id) =>
            Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await service.RemoveAsync(id)
            });
        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetById(long id) =>
            Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await service.RetrieveAsync(id)
            });
        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params) =>
            Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await service.RetrieveAll(@params)
            });
    }
}
