using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.Core;
using Clinic.Dtos;
using Clinic.Errors;
using Clinic.Extensions;
using Clinic.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Clinic.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : Controller
    {
        
        private readonly IEmployeeAccountRepository employeeAccountRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly AppSettings appSettings;

        public AuthController(IEmployeeAccountRepository employeeAccountRepository, IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            this.employeeAccountRepository = employeeAccountRepository;
            this.employeeRepository = employeeRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.appSettings = appSettings.Value;
        }

        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var user = employeeAccountRepository.Authenticate(model.Email, model.Password);

            if (user == null)
                return BadRequest(new ApiResponse(400, "Username or password is incorrect"));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Employee.FullName),
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var mapUser = mapper.Map<EmployeeAccount, UserDto>(user);
            mapUser.Token = tokenString;

            // return basic user info and authentication token
            return Ok(mapUser);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = await employeeRepository.GetEmployee(model.EmployeeId);

            // map model to entity
            var userMapper = mapper.Map<EmployeeAccount>(model);


            var user = employeeAccountRepository.Create(userMapper, model.Password);

            return Ok();
        }


    }
}

