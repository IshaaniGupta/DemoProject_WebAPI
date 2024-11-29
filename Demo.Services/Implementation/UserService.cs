using Demo.Domain.Model;
using Demo.Infra.DbContexts;
using Demo.Services.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Services.Implementation
{
    public class UserService : IUser
    {
        private readonly AppDbContext _db;
        public UserService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<string> LoginRepo(string username, string password)
        {
            try
            {
                var confirm_login = await _db.UserLogins
                .Where(x => x.Username == username && x.Password == password)
                .FirstOrDefaultAsync();

                if (confirm_login != null)
                {
                    return $"Welcome {username}, your password is {password}";
                }
                return $"Invlaid details";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public async Task<string> AddUserRepo(UserLogin user_detail)
        {
            try
            {
                await _db.UserLogins.AddAsync(user_detail);
                await _db.SaveChangesAsync();
                return user_detail.UserId.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public async Task<string> UpdateUserRepo(Guid user_id, UserLogin user_detail)
        {
            try
            {
                var get_detail = await _db.UserLogins
                .Where(x => x.UserId == user_id)
                .FirstOrDefaultAsync();

                if (get_detail != null)
                {
                    get_detail.Username = user_detail.Username;
                    get_detail.Password = user_detail.Password;

                    await _db.SaveChangesAsync();
                    return $"Successfully Updated";
                }
                return $"No Record(s) Found!!!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> DeleteUserRepo(Guid user_id)
        {
            try
            {
                var get_detail = await _db.UserLogins
                .Where(x => x.UserId == user_id)
                .FirstOrDefaultAsync();

                if (get_detail != null)
                {
                    _db.UserLogins.Remove(get_detail);
                    await _db.SaveChangesAsync();
                    return $"Successfully Deleted";
                }
                return $"No Record(s) Found!!!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
