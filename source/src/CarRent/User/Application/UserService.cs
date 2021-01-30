using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Common.Application;
using CarRent.User.Domain;

namespace CarRent.User.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _db;
        public UserService(IUserRepository db)
        {
            _db = db;
        }
        public async Task<UserDto> Get(int? id)
        {
            var data = await _db.Get(id);
            var mappedData = new UserDto(data);
            return mappedData;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var data = await _db.GetAll();
            var mappedData = data.Select(x => (new UserDto(x)));
            return mappedData;
        }

        public async Task<IEnumerable<UserDto>> Search(int? id, string name, string lastname)
        {
            var data = await _db.Search(id, name, lastname);
            var mappedData = data.Select(x => (new UserDto(x)));
            return mappedData;
        }

        public async Task<ResponseDto> Save(Domain.User user)
        {
            var responseDto = await _db.Save(user);
            return responseDto;
        }

        public async Task<ResponseDto> Delete(int? id)
        {
            var responseDto = await _db.Delete(id);
            return responseDto;
        }
    }
}
