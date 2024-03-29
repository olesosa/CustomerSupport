using AutoMapper;
using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using CS.DOM.DTO;
using CS.DOM.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CS.BL.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public UserService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> Create(UserSignUpDto userSignUpDto)
        {
            var user = _mapper.Map<User>(userSignUpDto);

            if (await _context.Users.AnyAsync(u => u.Email == userSignUpDto.Email))
            {
                throw new ApiException(400, "User already exist");
            }

            await _context.AddAsync(user);

            await _context.SaveChangesAsync();

            var createdUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
                
            var createdUserDto = _mapper.Map<UserDto>(createdUser);

            return createdUserDto;
        }

        public async Task<bool> Delete(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            
            if (user == null)
            {
                throw new ApiException(404, "User not found");
            }

            _context.Users.Remove(user);
            
            await _context.SaveChangesAsync();

            return true;
        }
    }
}