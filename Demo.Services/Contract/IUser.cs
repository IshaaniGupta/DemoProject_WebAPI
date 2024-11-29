using Demo.Domain.Model;
using System;
using System.Threading.Tasks;

namespace Demo.Services.Contract
{
    public interface IUser
    {
        Task<string> LoginRepo(string username, string password);
        Task<string> AddUserRepo(UserLogin user_detail);
        Task<string> UpdateUserRepo(Guid user_id, UserLogin user_detail);
        Task<string> DeleteUserRepo(Guid user_id);
    }
}