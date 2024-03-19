using CS.DAL.Models;
using CS.DOM.DTO;
using System.Diagnostics;

namespace CS.BL.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> Create(UserSignUpDto userSignUpDto);
    }
}
