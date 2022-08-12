using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RunningActivityTracker.Entities;
using RunningActivityTracker.Repositories;

namespace RunningActivityTracker.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;
        private IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }
        public UserEntity FindCurrentUser()
        {
            var autorizationHeader = _httpContextAccessor.HttpContext.Request.Headers.Authorization.ToString();
            string userInfoEncoded = new string(autorizationHeader.Skip(6).ToArray()); // Remove the "Basic " start of the header value


            string userInfoDecoded = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(userInfoEncoded));

            string userName = userInfoDecoded.Split(":")[0];
            string password = userInfoDecoded.Split(":")[1];
            return AuthenticateAsync(userName, password).Result;
        }

        public void CreateUser(UserEntity user)
        {
            _repository.CreateUser(user);
        }

        public Task<UserEntity> AuthenticateAsync(string userName, string password)
        {
            return Task.Run(() =>
                _repository.GetAll()
                    .FirstOrDefault(user => user.Value.Username == userName && user.Value.Password == password).Value);
        }

        public SortedList<string, UserEntity> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
