﻿namespace SP23.P02.Web.DTOs
{
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string[] Roles { get; set; }
        public string Password { get; set; } = string.Empty;

    }
}
