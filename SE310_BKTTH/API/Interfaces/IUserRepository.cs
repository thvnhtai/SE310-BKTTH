using API.Models;
using API.Models.Entity;

namespace API.Interfaces;

public interface IUserRepository
{
    IEnumerable<User> GetAllUsers();
    User GetUser(int id);
    User UpdateUser(int id, UpdateUserDto updateUserDto);
    bool DeleteUser(int id);
}
