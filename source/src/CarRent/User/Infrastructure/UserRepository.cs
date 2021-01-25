using System.Collections.Generic;
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

        public Task<List<Domain.User>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Domain.User>> Search(string name, string lastname)
        {
            throw new System.NotImplementedException();
        }

        public Task<ResponseDto> Save(Domain.User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<ResponseDto> Delete(int? id)
        {
            throw new System.NotImplementedException();
        }
    }
}
