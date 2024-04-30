using AutoMapper;
using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using CS.DOM.DTO;
using CS.DOM.Enums;
using CS.DOM.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CS.BL.Services;

public class UserService : IUserService
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public UserService(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDto> Create(UserInfoDto userInfoDto)
    {
        var user = _mapper.Map<User>(userInfoDto);

        if (await _context.Users.AnyAsync(u => u.Email == userInfoDto.Email))
        {
            throw new ApiException(400, "User already exist");
        }

        await _context.Users.AddAsync(user);

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

    public async Task<UserDto> GetById(Guid userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        return _mapper.Map<UserDto>(user);
    }

    public async Task<bool> IsUserExist(Guid userId)
    {
        return await _context.Users.AnyAsync(u => u.Id == userId);
    }

    public async Task<List<UserDto>> GetAllAdmins(CancellationToken cancellationToken)
    {
        var admins = await _context.Users.Where(u => u.RoleName == UserRoles.Admin).ToListAsync(cancellationToken);

        if (admins == null)
        {
            throw new ApiException(404, "No admins found");
        }

        return admins.Select(a => _mapper.Map<UserDto>(a)).ToList();
    }
}