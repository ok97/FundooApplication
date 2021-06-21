using Amazon.CognitoIdentityProvider.Model;
using BusinessLayer.Interfaces;
using CommonLayer;
using CommonLayer.RequestModel;
using CommonLayer.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserBL userBL;  //object IUser class
        
        public AccountController(IUserBL userBL )
        {
            this.userBL = userBL;
            
        }

        // Register User
        [HttpPost]
        [Route("Register")]
        public ActionResult AddUser(Users user)
        {
            try
            {
                this.userBL.AddUser(user);
                return this.Ok(new { success = true, Message = "User Registration successful" });
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { success = false, Message = ex.Message, InnerMessage = ex.InnerException });
            }
        }

        
        
        //Get data

       
        [HttpGet]
        [Route("Get")]
        public ActionResult GetUsersData()
        {
            try
            {
                List<Users> userData = userBL.GetUsersData();
                return Ok(userData.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }


        // User Login
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult LoginUser(Login login)
        {
            var token = this.userBL.Login(login.Email, login.Password);
            if (token == null)
                return Unauthorized();
            return this.Ok(new { token = token, success = true, message = "Token Generated Successfull" });
        }


        // Forgot Password

        [AllowAnonymous]
        [HttpPost("Forgot Password")]
        public ActionResult ForgotPassword(ForgotPassword user)
        {
            try
            {
                bool isExist = this.userBL.ForgotPassword(user.Email);
                if (isExist)
                {
                    Console.WriteLine($"Email User Exist with {user.Email}");
                    return Ok(new { success = true, message = $"Reset Link sent to {user.Email}" }); 
                }
                else                 
                {
                   
                    return BadRequest(new { success = false, message = $"No user Exist with {user.Email}" }); 
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //  Change Password

        [HttpPut("Reset Password")]
        public ActionResult ResetPassword(UserRestPassword user)
        {
            try
            {
                if (user.NewPassword != user.ConfirmPassword)
                {
                    return Ok(new { success = false, message = "New Password and Confirm Password are not equal." });
                }
                var UserEmailObject = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("Email", StringComparison.InvariantCultureIgnoreCase));
                this.userBL.ChangePassword(UserEmailObject.Value, user.NewPassword);                
                return Ok(new { success = true, message = "Password Sucessfully Changed", Email = $"{UserEmailObject.Value}" });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
