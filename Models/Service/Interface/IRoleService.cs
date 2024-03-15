using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MYMVC.Models.DTO;

namespace MYMVC.Models.Service.Interface
{
    public interface IRoleService
    {
        public Task<BaseResponse<RoleDto>> CreateAsync(RoleRequestModel role);
    }
}