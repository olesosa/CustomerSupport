using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CS.BL.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(ApplicationContext context) : base(context) { }
        public Task<bool> Create(User user) // need to be implemented
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
