using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CS.BL.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(ApplicationContext context) : base(context) { }


    }
}
