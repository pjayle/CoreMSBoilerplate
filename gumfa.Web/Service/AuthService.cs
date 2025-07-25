using gumfa.Web.Models;
using gumfa.Web.Models.DTO;
using gumfa.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace gumfa.Web.Service
{
    public interface IAuthService
    {
        Task<APIResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
        Task<APIResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<APIResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto);
    }

    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<APIResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.POST,
                Data = registrationRequestDto,
                Url = CONST.AuthAPIBase + "/api/auth/AssignRole"
            });
        }

        public async Task<APIResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.POST,
                Data = loginRequestDto,
                Url = CONST.AuthAPIBase + "/api/auth/login"
            }, withBearer: false);
        }

        public async Task<APIResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.POST,
                Data = registrationRequestDto,
                Url = CONST.AuthAPIBase + "/api/auth/register"
            }, withBearer: false);
        }
    }
}
