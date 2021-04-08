using BookStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services.Interface
{
    public interface IJwtTokenService
    {
        string CreateToken(User user);
    }
}
