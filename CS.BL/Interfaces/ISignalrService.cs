using CS.DOM.DTO;

namespace CS.BL.Interfaces;

public interface ISignalrService
{
    Task SendMessage(ChatMessageDto message, UserInfoDto user);
}