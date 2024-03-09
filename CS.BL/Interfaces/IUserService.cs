using CS.DAL.Models;
using CS.DOM.DTO;
using System.Diagnostics;

namespace CS.BL.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetById(Guid userId, CancellationToken cancellationToken = default);
        Task<bool> Create(UserSignUpDto userSignUpDto);
        Task<bool> DoEmailExist(string email);
    }
}
