using CS.DAL.Models;
using CS.DOM.DTO;
using System.Diagnostics;

namespace CS.BL.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> Create(UserSignUpDto userSignUpDto);
        Task<bool> Delete(Guid userId);
        Task<UserDto> GetById(Guid userId);
    }
}
