using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Entities
{
    public class User: IdentityUser
    {
        public virtual UserInfo UserInfo { get; set; }
    }
}
