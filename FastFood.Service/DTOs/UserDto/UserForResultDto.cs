﻿using FastFood.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Service.DTOs.UserDto
{
    public class UserForResultDto
    {
        [MaxLength(50)]
        public string Firstname { get; set; }

        [MaxLength(50)]
        public string Lastname { get; set; }

        [Required, MaxLength(50)]
        public string Phone { get; set; }

        [MinLength(5), MaxLength(50)]
        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
