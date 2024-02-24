using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;

namespace CS.BL.Services
{
    public class MassageService : BaseService<Message>, IMessageService
    {
        public MassageService(ApplicationContext context) : base(context) { }


    }
}
