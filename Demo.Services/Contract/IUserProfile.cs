using Demo.Domain.Model;
using System.Threading.Tasks;

namespace Demo.Services.Contract
{
    public interface IUserProfile
    {
        Task<UserProfile> UserProfileGetRepo(int ProfileId);
        Task<string> UserProfileAddRepo(UserProfile user_profile);
        Task<string> UserProfileUpdateRepo(int ProfileId, UserProfile user_profile);
        Task<string> UserProfileRemoveRepo(int ProfileId);
    }
}