using UsersApiApplication.Models;

namespace UsersApiApplication.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUser(Guid userId);
        Task<List<User>> AddUser(User user);
        Task<User> DeleteUser(Guid userId);

    }
}
