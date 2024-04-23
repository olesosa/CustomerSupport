using CS.BL.Helpers;
using CS.DOM.DTO;
using Microsoft.AspNetCore.Http;

namespace CS.BL.Interfaces;

public interface ISignalrService
{
    Task<MessageDto> SendMessage(PostMessage message, Guid dialogId ,UserInfoDto user);
}