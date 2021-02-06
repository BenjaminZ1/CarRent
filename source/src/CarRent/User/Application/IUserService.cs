using CarRent.Common.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRent.User.Application
{
    public interface IUserService
    {
        Task<UserDto> Get(int? id);
        Task<IEnumerable<UserDto>> GetAll();
        Task<IEnumerable<UserDto>> Search(int? id, string name, string lastname);
        Task<ResponseDto> Save(Domain.User car);
        Task<ResponseDto> Delete(int? id);
    }
}
