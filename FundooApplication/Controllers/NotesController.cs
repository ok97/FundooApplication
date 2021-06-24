using BusinessLayer.Interfaces;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundooApplication.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        INoteBL noteBL;
        public NotesController(INoteBL noteBl)
        {
            this.noteBL = noteBl;
        }
      [Authorize]
        [HttpPost("AddNote")]
        //[Route("")]
        public IActionResult CreateNote(AddNote userNoteData)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                int userID = Convert.ToInt32(idClaim.Value);
                UserNoteResponseData data = noteBL.CreateNote(userID, userNoteData);
                bool success = false;
                string message;
                if (userNoteData == null)
                {
                    message = "Try again";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    message = "Notes Created Successfully";
                    return Ok(new { success, message, data });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }





        // Get Notes using UserId

        [AllowAnonymous]
        [HttpGet("Get")]
        public ActionResult GetAllNotes(int UserId)
        {
            var result = this.noteBL.GetAllNotes(UserId);
            if (result != null) return this.Ok(new { success = true, message = $"List Of Notes with UserId: {UserId}.", data = result });
            return BadRequest(new { success = false, message = $"No such UserId Exist." });
        }



        // Delete Note Using NotesId

        [HttpDelete("Delete")]
        
        public IActionResult DeleteNote(int NotesId)
        {
            try
            {
                bool data = noteBL.DeleteNote(NotesId);
                bool success = false;
                string message;
                if (data)
                {
                    success = true;
                    message = "Note Deleted Successfully";
                    return Ok(new { success, message });
                }
                else
                {
                    message = "Try again";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }


        // Update Note

        [HttpPut("Update")]        
        public IActionResult UpdateNotes(int userId, int noteID, UpdateNoteRequest updateNoteRequest)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                
                UserNoteResponseData userUpdateData = noteBL.UpdateNote(userId, noteID, updateNoteRequest);
                bool success = false;
                string message;
                if (userUpdateData == null)
                {
                    message = "Try again";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    message = "Notes Updated Successfully";
                    return Ok(new { success, message, userUpdateData });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });

            }
        }
        // Add Reminder
        [HttpPut("Reminder")]
        
        public IActionResult AddReminder(int userId,int noteID, ReminderRequest reminder)
        {
            try
            {
                bool success = false, data;
                string message;
                //var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
               // int userId = Convert.ToInt32(idClaim.Value);
               data = noteBL.AddReminder(userId, noteID, reminder);

                
                if (data)
                {
                    success = true;
                    message = "Reminder Set Successfully";
                    return Ok(new { success, message });
                }
                else
                {
                    message = "Try Again!";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });

            }
        }

        // Add Color
        [HttpPut]
        [Route("Color")]
        public IActionResult AddColor(int userId, int noteID, ColorRequest color)
        {
            try
            {
                bool success = false, data;
                string message;
              //  var idClaim = HttpContext.User.Claims.FirstOrDefault(id => id.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                //int userId = Convert.ToInt32(idClaim.Value);
                data = noteBL.AddColor(userId, noteID, color);

                if (data)
                {
                    success = true;
                    message = "Color Set Successfully";
                    return Ok(new { success, message });
                }
                else
                {
                    message = "Try Again!";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });

            }
        }

        [HttpPut("Add Image")]
       
        public IActionResult AddImage(int userID,int noteID, ImageRequest imageRequest)
        {
            try
            {
                bool success = false;
                string message;
               
                bool data = noteBL.AddImage(userID, noteID, imageRequest);
                if (data)
                {
                    success = true;
                    message = "Image Added Successfully";
                    return Ok(new { success, message });
                }
                else
                {
                    message = "Unsuccessfull";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

    }
}
