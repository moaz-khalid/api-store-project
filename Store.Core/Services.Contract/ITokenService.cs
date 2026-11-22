using Microsoft.AspNetCore.Identity;
using Store.Core.Entities.identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Core.Services.Contract
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser user,UserManager<AppUser> userManager);


    }
}
