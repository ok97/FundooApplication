using Amazon.CognitoIdentityProvider.Model;
using BusinessLayer.Interfaces;
using CommonLayer;
using CommonLayer.RequestModel;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModel;
using FundooApplication.Contracts;
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
        //Declare private object for logger object
        private readonly ILoggerService _logger;
       

        private IUserBL userBL;  //object IUser class
        
        public AccountController(IUserBL userBL, ILoggerService logger)
        {
            this.userBL = userBL;
            _logger = logger; 

        }

        // Register User
        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult RegisterUser(RegisterUserRequest user)
        {
            try
            {
                _logger.LogInfo("Register Account Successfull");

                this.userBL.RegisterUser(user);
               string userFullName = user.FirstName + " " + user.LastName;
                
                return this.Ok(new { success = true, message = $"Hello {userFullName} Your Account Created Successfully {user.Email}" });
              
                
                

            }
            catch (Exception e)
            {
               
                return this.BadRequest(new { success = false, message = $"Registration Failed {e.Message}" });
               
                _logger.LogError("Registration Failed");
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
        public IActionResult Login(Login cred)
        {
            var token = this.userBL.Login(cred.Email, cred.Password);

            UserResponce data = new UserResponce();
           
            string message, userFullName;
            bool success = false;
            if (token == null)
            {
                _logger.LogWarn("Invalid Email and Password");
                message = "Enter Valid Email & Password";
                return Ok(new { success, message });
              
            }
            else 
            {
                _logger.LogInfo("Login user");
                success = true;
                userFullName = data.FirstName + " " + data.LastName;
                message = "Hello " + userFullName + ", You Logged in Successfully";
                return this.Ok(new { success = true, token = token, message = $"Login {cred.Email}" });
            }
        }

        [HttpGet("GetUser")]
        public string GetUser()
        {
            var UserEmail = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("Email", StringComparison.InvariantCultureIgnoreCase));
            var UserId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            return $" CLAIMS Email:{UserEmail.Value} UserId:{UserId.Value}";
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
                    _logger.LogInfo("Forgot Password");
                    Console.WriteLine($"Email User Exist with {user.Email}");
                    return Ok(new { success = true, message = $"Reset Link sent to {user.Email}" }); 
                }
                else                 
                {
                    _logger.LogError("Forgot Password Failed");
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
                if (user.NewPassword == user.ConfirmPassword)
                {
                    var EmailClaim = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("Email", StringComparison.InvariantCultureIgnoreCase));
                    this.userBL.ChangePassword(EmailClaim.Value, user.NewPassword);
                    return Ok(new { success = true, message = "Your Account Password Changed Successfully", Email = $"{EmailClaim.Value}" });

                }
                else
                {
                    _logger.LogWarn($"This is a warning ");
                    return Ok(new { success = false, message = "New Password and Confirm Password are not equal." });
                    
                }

                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



       

    }
}
