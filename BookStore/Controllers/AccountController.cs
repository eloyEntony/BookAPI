using BookStore.Models;
using BookStore.Models.Dto;
using BookStore.Models.Dto.ResultDto;
using BookStore.Models.Entities;
using BookStore.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AccountController(ApplicationContext context,
                                 UserManager<User> userManager,
                                 SignInManager<User> signInManager,
                                 IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
        }


        [HttpGet]
        public ResultDto Ok()
        {
            return new ResultDto { IsSuccessful = true, Message = "work" };
        }

        [HttpPost("register")]
        public async Task<ResultDto> Register([FromBody] RegisterDTO newR)
        {
            User user = new User() { Email = newR.Email, PhoneNumber = newR.PhoneNumber, UserName = newR.Email };

            UserInfo ui = new UserInfo() { Id = user.Id, Age = newR.Age, FullName = newR.FullName, Image = newR.Image };

            //var result = _userManager.AddToRoleAsync(user, "Guest").Result;

            await _userManager.CreateAsync(user, newR.Password);

            await _context.UserInfos.AddAsync(ui);
            await _context.SaveChangesAsync();

            return new ResultDto { IsSuccessful = true, Message = "register good" };
        }

        [HttpPost("login")]
        public async Task<ResultDto> Login(LoginDTO model)
        {
            var res = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (!res.Succeeded)
                return new ResultDto() { IsSuccessful = false, Message = "error data" };



            var user = await _userManager.FindByEmailAsync(model.Email);
            await _signInManager.SignInAsync(user, isPersistent: false);

            //return new ResultDto() { IsSuccessful = true, Message = "good" };
            return new ResultLoginDTO
            {
                IsSuccessful = true,
                Token = _jwtTokenService.CreateToken(user)
            };
        }

    }
}
