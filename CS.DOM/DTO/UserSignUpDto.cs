using System.ComponentModel.DataAnnotations;

namespace CS.DOM.DTO
{
    public class UserSignUpDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
