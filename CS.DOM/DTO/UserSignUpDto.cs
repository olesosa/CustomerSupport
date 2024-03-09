using System.ComponentModel.DataAnnotations;

namespace CS.DOM.DTO
{
    public class UserSignUpDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
