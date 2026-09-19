using Microsoft.AspNetCore.Identity;
using Store.Core.Entities.identity;
using Store.Repository.identity.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Repository.identity
{
    public class StoreIdentityDbContextSeed
    {
        public static async Task SeedAppUserAsync(UserManager<AppUser> _userManager)
        {

            if (_userManager.Users.Count() == 0) 

            {
                var user = new AppUser
                {
                    Email = "bob@example.com",
                    DisplayName = "Bob",
                    UserName = "bob",
                    PhoneNumber = "1234567890",
                    Address = new Address
                    {
                        FName = "Bob",
                        LName = "Smith",
                        City = "New Cairo",
                        State = "Cairo",
                        Country = "Egypt",
                        Street = "123 Main St",
                    }

                };
                await _userManager.CreateAsync(user, "Password123!" );

            }

        }

    }
}
