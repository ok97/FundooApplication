using Amazon.CognitoIdentityProvider.Model;
using BusinessLayer.Interfaces;
using CommonLayer;
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


        //[HttpPost]
        //public ActionResult SampleApi(Users newUser)
        //{
        //    try
        //    {
        //        bool result = this.userBL.SampleApi(newUser);
        //        if (result == true)
        //        {
        //            return this.Ok(new { success = true, Message = "User Registration successful"});
        //        }
        //        else
        //        {
        //            return this.Ok(new { success = false, Message = "User Registration is not successful" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return this.BadRequest(new { success = false, Message = ex.Message, InnerMessage = ex.InnerException });
        //    }

        //}

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

        
        
        //get data

       
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
        [HttpPost("login")]
        public IActionResult LoginUser(EmailModle emailModel)
        {
            var token = this.userBL.Login(emailModel.Email, emailModel.Password);
            if (token == null)
                return Unauthorized();
            return this.Ok(new { token = token, success = true, message = "Token Generated Successfull" });
        }

    }
}
