using Microsoft.EntityFrameworkCore;
using UsersApiApplication.Data;
using UsersApiApplication.Models;

namespace UsersApiApplication.Services
{
    public class UserService : IUserService
    {
        HashSet<string> usersAdding;
        private readonly DataContext dataContext;
        public UserService(DataContext context)
        {
            dataContext = context;
            usersAdding = new HashSet<string>();
        }
        private async Task<bool> CheckIsAdminAdded()
        {
            var selected = from userGroup in dataContext.UsersGroup
                           where userGroup.Code == UserGroups.Admin
                           select userGroup;
            return await selected.AnyAsync();
        }

        private async Task<bool> CheckIsLoginPresented(string login)
        {
            var selected = from user in dataContext.Users
                           where user.Login == login
                           select user;
            return await selected.AnyAsync();
        }

        public async Task<List<User>> AddUser(User user)
        {
            if (user.UserGroup.Code == UserGroups.Admin && await CheckIsAdminAdded())
                throw new BadHttpRequestException("Only one user is allowed to have userGroup.Admin");
            if (usersAdding.Contains(user.Login) && await CheckIsLoginPresented(user.Login))
                throw new BadHttpRequestException("This login is already used");

            usersAdding.Add(user.Login);
            Task delay = Task.Delay(5000);
            user.ID = Guid.NewGuid();
            user.UserGroupID = Guid.NewGuid();
            user.UserStateID = Guid.NewGuid();
            user.UserGroup.ID = user.UserGroupID;
            user.UserState.ID = user.UserStateID;
            user.UserState.Code = UserStates.Active;
            dataContext.UsersGroup.Add(user.UserGroup);
            dataContext.UsersState.Add(user.UserState);
            dataContext.Users.Add(user);
            Task savingChanges = dataContext.SaveChangesAsync();
            usersAdding.Remove(user.Login);
            await Task.WhenAll(new Task[] { delay, savingChanges });

            return await GetAllUsers();
        }

        public async Task<User> DeleteUser(Guid userId)
        {
            var user = await dataContext.Users.FindAsync(userId);
            if (user is null)
                throw new KeyNotFoundException($"User: {userId} can't be found");
            var userState = await dataContext.UsersState.FindAsync(user.UserStateID);
            if (userState is null)
                throw new KeyNotFoundException();
            userState.Code = UserStates.Blocked;
            await dataContext.SaveChangesAsync();

            user.UserGroup = await dataContext.UsersGroup.FindAsync(user.UserGroupID);
            user.UserState = userState;
            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = from user in dataContext.Users
                        join userGroup in dataContext.UsersGroup on user.UserGroupID equals userGroup.ID
                        join userState in dataContext.UsersState on user.UserStateID equals userState.ID
                        select new User()
                        {
                            ID = user.ID,
                            Login = user.Login,
                            Password = user.Password,
                            CreatedDate = user.CreatedDate,
                            UserGroupID = userGroup.ID,
                            UserGroup = userGroup,
                            UserStateID = userState.ID,
                            UserState = userState
                        };
            return await users.ToListAsync();
        }

        public async Task<User> GetUser(Guid userId)
        {
            var user = await dataContext.Users.FindAsync(userId);
            if (user is null)
                throw new KeyNotFoundException($"User: {userId} can't be found");
            user.UserGroup = await dataContext.UsersGroup.FindAsync(user.UserGroupID);
            user.UserState = await dataContext.UsersState.FindAsync(user.UserStateID);
            return user;
        }
    }
}
