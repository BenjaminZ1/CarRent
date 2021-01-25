using System.Collections.Generic;
using System.Threading.Tasks;
using CarRent.Common.Application;

namespace CarRent.User.Domain
{
    interface IUserRepository
    {
        Task<User> Get(int? id);
        Task<List<User>> GetAll();
        Task<List<User>> Search(string name, string lastname);
        Task<ResponseDto> Save(User user);
        Task<ResponseDto> Delete(int? id);
    }
}
