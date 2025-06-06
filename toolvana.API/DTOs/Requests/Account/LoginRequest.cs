﻿using System.ComponentModel.DataAnnotations;

namespace toolvana.API.DTOs.Requests.Account
{
    public class LoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
