using System;
using System.Collections;
using System.Linq;
using API.Data;
using API.Interfaces;
using API.Models;
using API.Models.Entity;

namespace API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }
    
    public IEnumerable<User> GetAllUsers()
    {
        return _context.Users.OrderBy(u => u.Id).ToList();
    }

    public User GetUser(int id)
    {
        throw new NotImplementedException();
    }

    public User UpdateUser(int id, UpdateUserDto updateUserDto)
    {
        
        throw new NotImplementedException();
    }

    public bool DeleteUser(int id)
    {
        throw new NotImplementedException();
    }
}
