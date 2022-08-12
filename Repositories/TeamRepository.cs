using System.Collections.Generic;
using RunningActivityTracker.Entities;

namespace RunningActivityTracker.Repositories
{
    public class TeamRepository : ITeamRepository
    {

        public TeamRepository()
        {
            Teams ??= new SortedList<string, TeamEntity>();
        }
        public SortedList<string, TeamEntity> Teams { get; set; }

        public TeamEntity FindTeamByAdmin(string userName)
        {
            foreach (var pair in Teams)
            {
                if (pair.Value.Admin.Username == userName)
                {
                    return pair.Value;
                }
            }

            return null;
        }

        public TeamEntity FindTeamByMember(string userName)
        {
            foreach (var pair in Teams)
            {
                foreach (var member in pair.Value.Members)
                {
                    if (member.Username == userName)
                    {
                        return pair.Value;
                    }
                }
            }

            return null;
        }

        public void CreateTeam(string name, List<UserEntity> members, UserEntity admin)
        {
            var team = new TeamEntity(name, members, admin);
            Teams.Add(team.Name, team);
        }

        public void Clear()
        {
            Teams = new SortedList<string, TeamEntity>();
        }

        public void AddTeamMember(TeamEntity team, UserEntity member)
        {
            foreach (var pair in Teams)
            {
                if (pair.Value == team)
                {
                    team.Members.Add(member);
                }
            }
        }

        public SortedList<string, TeamEntity> GetAll()
        {
            return Teams;
        }
    }
}
