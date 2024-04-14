using CS.DOM.DTO;

namespace CS.BL.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> Create(UserInfoDto userInfoDto);
        Task<bool> Delete(Guid userId);
        Task<UserDto> GetById(Guid userId);
        Task<bool> IsUserExist(Guid userId);
    }
}
