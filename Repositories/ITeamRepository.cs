using System.Collections.Generic;
using RunningActivityTracker.Entities;

namespace RunningActivityTracker.Repositories
{
    public interface ITeamRepository
    {
        TeamEntity FindTeamByAdmin(string userName);
        TeamEntity FindTeamByMember(string userName);
        void CreateTeam(string team, List<UserEntity> members, UserEntity user);
        void Clear();
        void AddTeamMember(TeamEntity team, UserEntity member);
        SortedList<string, TeamEntity> GetAll();
    }
}
