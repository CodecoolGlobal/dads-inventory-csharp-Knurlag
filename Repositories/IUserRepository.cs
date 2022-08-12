using System.Collections.Generic;
using RunningActivityTracker.Entities;

namespace RunningActivityTracker.Repositories
{
    public interface IUserRepository
    {
        UserEntity FindUserByName(string userName);
        void CreateUser(UserEntity name);
        SortedList<string, UserEntity> GetAll();
        void Clear();
    }
}
