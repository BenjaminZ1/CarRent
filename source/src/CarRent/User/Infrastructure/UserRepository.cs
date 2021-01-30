using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Common.Application;
using CarRent.User.Domain;
using Microsoft.EntityFrameworkCore;

namespace CarRent.User.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _db;

        public UserRepository(UserDbContext db)
        {
            _db = db;
        }

        public async Task<Domain.User> Get(int? id)
        {
            var user = new Domain.User();
            if (id != null)
            {
                user = await _db.User
                    .FirstOrDefaultAsync(u => u.Id == id);
            }
            return user;
        }

        public async Task<List<Domain.User>> GetAll()
        {
            var users = await _db.User.ToListAsync();
            return users;
        }

        public async Task<List<Domain.User>> Search(int? id, string name, string lastname)
        {
            IQueryable<Domain.User> query = _db.User;

            if (id != null & string.IsNullOrEmpty(name) & string.IsNullOrEmpty(lastname))
            {
                query = query.Where(u => u.Id == id);
            }
            else if (id != null & !string.IsNullOrEmpty(name) & string.IsNullOrEmpty(lastname))
            {
                query = query.Where(u => u.Id == id & u.Name.Contains(name));
            }
            else if (id != null & string.IsNullOrEmpty(name) & !string.IsNullOrEmpty(lastname))
            {
                query = query.Where(u => u.Id == id & u.LastName.Contains(lastname));
            }
            else if (id == null & !string.IsNullOrEmpty(name) & string.IsNullOrEmpty(lastname))
            {
                query = query.Where(u => u.Name.Contains(name));
            }
            else if (id == null & string.IsNullOrEmpty(name) & !string.IsNullOrEmpty(lastname))
            {
                query = query.Where(u => u.LastName.Contains(lastname));
            }
            else if (id != null & !string.IsNullOrEmpty(name) & !string.IsNullOrEmpty(lastname))
            {
                query = query.Where(u => u.Id == id & u.Name.Contains(name) & u.LastName.Contains(lastname));
            }

            return await query.ToListAsync();
        }

        public async Task<ResponseDto> Save(Domain.User user)
        {
            ResponseDto responseDto = new ResponseDto();
            if (user.Id == 0)
            {
                try
                {

                    await _db.AddAsync(user);
                    await _db.SaveChangesAsync();

                    responseDto.Id = user.Id;
                    responseDto.Flag = true;
                    responseDto.Message = "Has Been Added.";
                }
                catch (Exception e)
                {
                    responseDto.Flag = false;
                    responseDto.Message = e.ToString();
                }
            }
            else if (user.Id != 0)
            {
                Domain.User entity = await Get(user.Id);
                entity.Id = user.Id;
                entity.Name = user.Name;
                entity.LastName = user.LastName;
                entity.Street = user.Street;
                entity.Place = user.Place;
                entity.Plz = user.Plz;

                try
                {
                    await _db.SaveChangesAsync();
                    responseDto.Id = user.Id;
                    responseDto.Flag = true;
                    responseDto.Message = "Has Been Updated.";
                }
                catch (Exception e)
                {
                    responseDto.Flag = false;
                    responseDto.Message = e.ToString();
                }
            }
            return responseDto;
        }

        public async Task<ResponseDto> Delete(int? id)
        {
            ResponseDto responseDto = new ResponseDto();
            Domain.User user = await Get(id);

            if (user != null)
            {
                try
                {
                    _db.User.Remove(user);

                    await _db.SaveChangesAsync();

                    responseDto.Flag = true;
                    responseDto.Message = "Has been Deleted.";
                }
                catch (Exception e)
                {
                    responseDto.Flag = false;
                    responseDto.Message = e.ToString();
                }
            }
            else
            {
                responseDto.Flag = false;
                responseDto.Message = "User does not exist.";
            }

            return responseDto;
        }
    }
}
