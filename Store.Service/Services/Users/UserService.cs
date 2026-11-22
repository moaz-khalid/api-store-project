using Microsoft.AspNetCore.Identity;
using Store.Core.Dtos.Auth;
using Store.Core.Entities.identity;
using Store.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Service.Services.Users
{
    public class UserService : IUserService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private  readonly UserManager<AppUser> _UserManager;
        private readonly ITokenService _tokenService;

        public UserService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService
            )
        {
            _UserManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _UserManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return null;
            var resault = await _signInManager.CheckPasswordSignInAsync(user,loginDto.Password,false);

            if (!resault.Succeeded) return null;

            return new UserDto()
            {
                DisplayName =user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user,_UserManager)


            };

        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            if (await CheckEmailExistAsync(registerDto.Email)) return null;

            var user = new AppUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email.Split("@")[0] 
            };

            var result = await _UserManager.CreateAsync(user ,registerDto.Password);

            if (!result.Succeeded) return null;

            return new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateTokenAsync(user, _UserManager)
            };
        }

        public async Task<bool> CheckEmailExistAsync(string email)
        {
            return await _UserManager.FindByEmailAsync(email) != null; 
        }


    }
}
