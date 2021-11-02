using System;
using System.Collections.Generic;
using Apsi.Database.Entities;
using Apsi.Database.Entities.Enums;

namespace apsi.backend.social.Models
{
    public class LoggedUserDto
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole UserRole { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpirationDate { get; set; }
    }
}