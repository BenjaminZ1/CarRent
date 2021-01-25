using System.Collections.Generic;
using System.Threading.Tasks;
using CarRent.Common.Application;

namespace CarRent.User.Application
{
    public class UserService : IUserService
    {
        public Task<UserDto> Get(int? id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> Search(string brand, string model)
        {
            throw new System.NotImplementedException();
        }

        public Task<ResponseDto> Save(Domain.User car)
        {
            throw new System.NotImplementedException();
        }

        public Task<ResponseDto> Delete(int? id)
        {
            throw new System.NotImplementedException();
        }
    }
}
