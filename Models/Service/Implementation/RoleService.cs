using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MYMVC.Models.DTO;
using MYMVC.Models.Entities;
using MYMVC.Models.Repository.Interface;
using MYMVC.Models.Service.Interface;

namespace MYMVC.Models.Service.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<RoleDto>> CreateAsync(RoleRequestModel role)
        {
            var newRole = new Role
            {
                Description = role.Description,
                RoleName = role.RoleName,
            };
            var createRole = await _roleRepository.CreateAsync(newRole);
            await _unitOfWork.SaveAsync();
            if (createRole != null)
            {
                return new BaseResponse<RoleDto>
                {
                    Data = new RoleDto
                    {
                        Description = createRole.Description,
                        Id = createRole.Id,
                        RoleName = createRole.RoleName
                    },
                    Message = "Created successfully",
                    Status = true
                };
            }
            return new BaseResponse<RoleDto>
            {
                Data = null,
                Message = "Not created",
                Status = false
            };
        }
    }
}