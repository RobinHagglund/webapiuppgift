using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapiuppgift.Data;
using webapiuppgift.Models.User;


namespace webapiuppgift.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> UpdateUserAsync(int id, UserUpdateRequest request);
        Task<User> DeleteAsync(int id);
    }
    public class UserService : IUserService
    {
        private readonly DatebaseContext _db;
        private readonly IMapper _map;

        public UserService(DatebaseContext db, IMapper map)
        {
            _db = db;
            _map = map;
        }

        public async Task<User> DeleteAsync(int id)
        {
            var userEntity = await _db.Users.FindAsync(id);
            if (userEntity != null)
            {
                userEntity.FirstName = "Bortagen";
                userEntity.LastName = "Bortagen";
                userEntity.Adress = "Bortagen";
                userEntity.ZipCode = "Bortagen";
                userEntity.City = "Bortagen";

                _db.Entry(userEntity).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return _map.Map<User>(userEntity);

           }
            return null!;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return _map.Map<IEnumerable<User>>(await _db.Users.ToListAsync());
        }


        public async Task<User> UpdateUserAsync(int id, UserUpdateRequest request)
        {
            var userEntity = await _db.Users.FindAsync(id);
            if (userEntity != null)
            {
                if (userEntity.FirstName != request.FirstName && !string.IsNullOrEmpty(request.FirstName))
                    userEntity.FirstName = request.FirstName;

                if (userEntity.LastName != request.LastName && !string.IsNullOrEmpty(request.LastName))
                    userEntity.LastName = request.LastName;

                if (userEntity.Adress != request.Adress && !string.IsNullOrEmpty(request.Adress))
                    userEntity.Adress = request.Adress;

                if (userEntity.ZipCode != request.ZipCode && !string.IsNullOrEmpty(request.ZipCode))
                    userEntity.ZipCode = request.ZipCode;

                if (userEntity.City != request.City && !string.IsNullOrEmpty(request.City))
                    userEntity.City = request.City;

                if (userEntity.Role != request.Role && !string.IsNullOrEmpty(request.Role))
                    userEntity.Role = request.Role;

                _db.Entry(userEntity).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return _map.Map<User>(userEntity);

            }
            return null!;
        }
    }
}
