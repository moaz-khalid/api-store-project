using Microsoft.AspNetCore.Identity;
using Store.Core.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Store.Core.Services.Contract
{
    public interface IUserService
    {
        Task<UserDto> LoginAsync(LoginDto loginDto);

        Task<UserDto> RegisterAsync(RegisterDto registerDto);

        Task<bool> CheckEmailExistAsync(string email);


    }
}
