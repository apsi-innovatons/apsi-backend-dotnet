using Apsi.Database.Entities.Enums;

namespace apsi.backend.social.Models
{
    public class UserDto
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole UserRole { get; set; }
    }
}