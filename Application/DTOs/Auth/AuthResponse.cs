using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Auth
{
    public class AuthResponse
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
