using BusinessLayer.Interfaces;
using CommonLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserBL userBL;  //object IUser class
        public AccountController(IUserBL userBL)
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
        

    }
}
