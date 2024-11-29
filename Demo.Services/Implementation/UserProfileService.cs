using Demo.Domain.Model;
using Demo.Infra.DbContexts;
using Demo.Services.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Services.Implementation
{
    public class UserProfileService : IUserProfile
    {
        private readonly AppDbContext _db;
        public UserProfileService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<UserProfile> UserProfileGetRepo(int ProfileId)
        {
            UserProfile userProfile = null;
            try
            {

                var get_user_profile = await _db.UserProfiles
                .Where(x => x.ProfileId == ProfileId).AsNoTracking()
                .FirstOrDefaultAsync();

                if (get_user_profile != null)
                {
                    userProfile = get_user_profile;
                }
                return null;
            }
            catch (Exception ex)
            {
            }
            return userProfile;

        }
        public async Task<string> UserProfileAddRepo(UserProfile user_profile)
        {
            try
            {
                await _db.UserProfiles.AddAsync(user_profile);
                await _db.SaveChangesAsync();
                return user_profile.ProfileId.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public async Task<string> UserProfileUpdateRepo(int ProfileId, UserProfile user_profile)
        {
            try
            {
                var get_detail = await _db.UserProfiles
                .Where(x => x.ProfileId == ProfileId)
                .FirstOrDefaultAsync();

                if (get_detail != null)
                {
                    get_detail.FirstName = user_profile.FirstName;
                    get_detail.LastName = user_profile.LastName;
                    get_detail.Email = user_profile.Email;
                    get_detail.PhoneNumber = user_profile.PhoneNumber;
                    get_detail.Address = user_profile.Address;

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
        public async Task<string> UserProfileRemoveRepo(int ProfileId)
        {
            try
            {
                var get_detail = await _db.UserProfiles
                .Where(x => x.ProfileId == ProfileId)
                .FirstOrDefaultAsync();

                if (get_detail != null)
                {
                    _db.UserProfiles.Remove(get_detail);
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