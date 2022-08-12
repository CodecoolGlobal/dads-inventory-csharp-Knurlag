using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using RunningActivityTracker.Entities;
using RunningActivityTracker.Repositories;

namespace RunningActivityTracker.Services
{
    public class TeamService : ITeamService
    {

        private ITeamRepository _repository;
        private IUserService _userService;

        public TeamService(ITeamRepository repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

    public void CreateTeam(string teamName)
    {
            var user = _userService.FindCurrentUser();
            List<UserEntity> members = new List<UserEntity>{user};
            _repository.CreateTeam(teamName, members, user);
    }

        public void AddMember(string memberName)
        {
            UserEntity user = new UserEntity();

            var users = _userService.GetAll();
            foreach (var userPair in users)
            {
                if (userPair.Value.Username == memberName)
                {
                    user = userPair.Value;
                    userPair.Value.Roles.Append("Member");
                }   
            }
            var currentUser = _userService.FindCurrentUser();
            var team =_repository.FindTeamByAdmin(currentUser.Username);
            team.Members.Add(user);
        }

        public SortedList<string, TeamEntity> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
