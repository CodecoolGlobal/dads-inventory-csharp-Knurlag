using System.Collections.Generic;
using RunningActivityTracker.Entities;

namespace RunningActivityTracker.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository()
        {
            if (Users == null)
            {
                Users ??= new SortedList<string, UserEntity>();
                var dad = new UserEntity{Email = "asd@asd.com", Password = "Dad", Roles = new []{"Dad"} , Username = "Dad"};
                var mom = new UserEntity { Email = "asd@asd.com", Password = "Mom", Roles = new[] { "Mom" }, Username = "Mom" };
                CreateUser(dad);
                CreateUser(mom);
            }


        }

        public SortedList<string, UserEntity> Users { get; set; }

        public UserEntity FindUserByName(string userName)
        {
            foreach (var user in Users)
            {
                if (user.Key == userName)
                {
                    return user.Value;
                }
            }

            return null;
        }

        public void CreateUser(UserEntity user)
        {
            Users.Add(user.Username, user);
        }

        public SortedList<string, UserEntity> GetAll()
        {
            return Users;
        }

        public void Clear()
        {
            Users = new SortedList<string, UserEntity>();
        }
    }
}
