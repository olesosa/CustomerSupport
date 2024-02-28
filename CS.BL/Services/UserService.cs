using AutoMapper;
using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CS.BL.Services
{
    public class UserService : IUserService
    {
        readonly ApplicationContext _context;
        readonly IMapper _mapper;
        public UserService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        private async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();

            return saved > 0 ? true : false;
        }

        // TODO: need to be implemented
        public Task<bool> Create(User user)
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
