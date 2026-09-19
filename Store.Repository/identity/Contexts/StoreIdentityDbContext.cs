using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.Core.Entities.identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Repository.identity.Contexts
{
    public class StoreIdentityDbContext: IdentityDbContext<AppUser>
    {
        public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options) : base(options)
        {
            
        }


    }
}
