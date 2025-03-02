using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using StoriesSpain.Models;
using StoriesSpain.Services;
using StoriesSpain.DTOs;


namespace StoriesSpain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;


        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, EmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _configuration = configuration;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Generate an email verification token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                // Create the verification link
                var verificationLink = Url.Action("VerifyEmail", "Account", new
                {
                    userId = user.Id,
                    token = token
                }, Request.Scheme);
                // Send the verification email
                var emailSubject = "Email Verification";
                var emailBody = $"Please verify your email by clicking the following link: {verificationLink}";
                _emailService.SendEmail(user.Email, emailSubject, emailBody);

                return Ok("User registered successfully. An email verification link has been sent.");
            }
            return BadRequest(result.Errors);
        }

        //handles email verification
        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok("Email confirmed successfully.");
            }
            return BadRequest("Email confirmation failed.");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,
isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var roles = await _userManager.GetRolesAsync(user);
                var token = GenerateJwtToken(user, roles);
                return Ok(new { Token = token });
            }
            return Unauthorized("Invalid login attempt.");
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logged out");
        }




        // Generate JWT Token
        private string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            var Claims = new List<Claim>
     {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // Add user roles to the token
            foreach (var role in roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "DefaultSecretKey1234567890"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(Convert.ToDouble(_configuration["Jwt:ExpireHours"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                Claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        // [HttpDelete("delete-user/{email}")]
        // public async Task<IActionResult> DeleteUser(string email)
        // {
        //     var user = await _userManager.FindByEmailAsync(email);
        //     if (user == null)
        //     {
        //         return NotFound("User not found.");
        //     }

        //     var result = await _userManager.DeleteAsync(user);
        //     if (result.Succeeded)
        //     {
        //         return Ok("User deleted successfully.");
        //     }

        //     return BadRequest("Error deleting user.");
        // } used to de-register while practicing.

    }
}}