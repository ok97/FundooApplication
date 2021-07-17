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
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using FundooApplication.Contracts;

namespace FundooApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        //Declare private object for logger object
        private readonly ILoggerService _logger;
        INoteBL noteBL;
        public NotesController(INoteBL noteBl, ILoggerService logger)
        {
            this.noteBL = noteBl;
            _logger = logger;
        }
      //[Authorize]
        [HttpPost("AddNote")]
        //[Route("")]
        public IActionResult CreateNote(AddNote userNoteData)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int userID = Convert.ToInt32(idClaim.Value);
                UserNoteResponseData data = noteBL.CreateNote(userID, userNoteData);
                bool success = false;
                string message;
                if (userNoteData == null)
                {
                    _logger.LogError("Create Note Failed"); // Logger Error
                    message = $"Create Note Failed";
                    return Ok(new { success, message });
                   
                }
                else
                {
                    _logger.LogInfo($"Notes Created Successfully {userID}"); // Logger Info
                    success = true;
                    message = $"Notes Created Successfully {userID}";
                    return Ok(new { success, message, data });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        // Get Notes using UserId

        [HttpGet("GetNotes")]
        public ActionResult GetAllNotes()
        {
            var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            int UserId = Convert.ToInt32(idClaim.Value);         
            var result = noteBL.GetAllNotes(UserId);
            if (result != null)
            {
                _logger.LogInfo($"Get Notes Successfully Thise UserId:- {UserId}"); //Logger 
                return this.Ok(new { success = true, message = $"List Of Notes with UserId: {UserId}.", data = result });
            }
            return BadRequest(new { success = false, message = $"No such UserId Exist." });
        }


        // Delete Note Using NotesId
        //[AllowAnonymous]

        [HttpDelete("Delete/{notesId}")]

        public IActionResult DeleteNote(int notesId)
        {
            try
            {
                bool data = noteBL.DeleteNote(notesId);
                bool success = false;
                string message;
                if (data)
                {
                    _logger.LogInfo($"Delete Notes Successfully thise NotesId:- {notesId}"); //Logger
                    success = true;
                    message = "Note Deleted Successfull";
                    return Ok(new { success, message });
                }
                else
                {
                    _logger.LogError("delete Note Failed"); // Logger Error
                    message = $"Delete Failed";
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
        public IActionResult UpdateNotes(UpdateNoteRequest updateNoteRequest)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Convert.ToInt32(idClaim.Value);
                UserNoteResponseData userUpdateData = noteBL.UpdateNote( updateNoteRequest);
                bool success = false;
                string message;
                if (userUpdateData == null)
                {
                    message = $"Update Failed";
                    return Ok(new { success, message });
                }
                else
                {
                    _logger.LogInfo($"Update Notes Successfully {userUpdateData}"); //Logger 
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

        // Update Title
        [HttpPut("Update Title")]
        public IActionResult UpdateTitle(int noteID, AddTitle title)
        {
            try
            {
                bool success = false, data;
                string message;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(userId => userId.Type.Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);
                data = noteBL.UpdateTitle(userId, noteID, title);


                if (data)
                {
                    success = true;
                    message = "Title Update Successfully";
                    return Ok(new { success, message });
                }
                else
                {
                    message = $"Update Failed";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });

            }
        }

        // Update Body
        [HttpPut("Body")]
        public ActionResult UpdateBody(int noteId, AddBody addBody)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(userId => userId.Type.Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);
                this.noteBL.UpdateBody(userId, noteId, addBody);
                return Ok(new { success = true, message = $"Body Updated Successfully" });
            }
            catch (Exception)
            {
                return BadRequest(new { success = false, message = $"Update Failed" });
            }
        }

        // Update Reminder
        [HttpPut("Reminder")]
        public IActionResult UpdateReminder(int noteID, ReminderRequest reminder)
        {
            try
            {
                bool success = false, data;
                string message;

                var idClaim = HttpContext.User.Claims.FirstOrDefault(userId => userId.Type.Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);
                data = noteBL.UpdateReminder(userId, noteID, reminder);


                if (data)
                {
                    success = true;
                    message = "Reminder Update Successfull";
                    return Ok(new { success, message });
                }
                else
                {
                    message = $"Update Failed";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });

            }
        }

        // Update Color
        [HttpPut]
        [Route("Color")]
        public IActionResult UpdateColor( int noteID, ColorRequest color)
        {
            try
            {
                bool success = false, data;
                string message;

                var idClaim = HttpContext.User.Claims.FirstOrDefault(userId => userId.Type.Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);
                data = noteBL.UpdateColor(userId, noteID, color);

                if (data)
                {
                    success = true;
                    message = "Color Set Successfully";
                    return Ok(new { success, message });
                }
                else
                {
                    message = $"Update Failed";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });

            }
        }
        // Update Image
        [HttpPut("Image")]
        public IActionResult UpdateImage(int noteID, ImageRequest imageRequest)
        {
            try
            {
                bool success = false;
                string message;

                var idClaim = HttpContext.User.Claims.FirstOrDefault(userId => userId.Type.Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);
                bool data = noteBL.UpdateImage(userId, noteID, imageRequest);
                if (data)
                {
                    success = true;
                    message = "Image Update Successfull";
                    return Ok(new { success, message });
                }
                else
                {
                    message = $"Update Failed";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        // Update Archived
        [HttpPut("Archived")]
        public ActionResult UpdateArchived( AddArchived UpdateArchived)
        {
            try
            {
                this.noteBL.UpdateArchived( UpdateArchived);
                return Ok(new { success = true, message = $"Archived Update Successfull" });
            }
            catch (Exception)
            {
                return BadRequest(new { success = false, message = $"Update Failed" });
            }
        }

        // Update Trash
        [HttpPut("Trash")]
        public ActionResult UpdateTrash(int noteId, AddTrash UpdateTrash)
        {
            try
            {
                this.noteBL.UpdateTrash(noteId, UpdateTrash.Trash);
                return Ok(new { success = true, message = $"Trash Update Successfull" });
            }
            catch (Exception)
            {
                return BadRequest(new { success = false, message = $"Update Failed" });
            }
        }
        // Update Pin
        [HttpPut("Pin")]
        public ActionResult UpdatePin(int noteId, AddPin UpdatePin)
        {
            try
            {
                this.noteBL.UpdatePin(noteId, UpdatePin.Pin);
                return Ok(new { success = true, message = $"Pin Update Successfull" });
            }
            catch (Exception)
            {
                return BadRequest(new { success = false, message = $"Update Failed" });
            }
        }

        // Get All Trashed Notes
        [HttpGet]
        [Route("Trash")]
        public IActionResult GetAllTrashedNotes()
        {
            try
            {
                bool success = false;
                string message;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(userId => userId.Type.Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);
                List<UserNoteResponseData> userNoteResponseDataList = noteBL.GetTrashedNotes(userId);

                if (userNoteResponseDataList != null)
                {
                    return Ok(userNoteResponseDataList.ToList());
                }
                else
                {
                    message = "No Trashed Notes";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        // Get All Archieved Notes

        [HttpGet]
        [Route("Archieve")]
        public IActionResult GetAllArchievedNotes()
        {
            try
            {
                string message;
                bool success = false;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(userId => userId.Type.Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);

                List<UserNoteResponseData> userNoteResponseDataList = noteBL.GetArchievedNotes(userId);

                if (userNoteResponseDataList != null)
                {
                    return Ok(userNoteResponseDataList.ToList());
                }
                else
                {
                    message = "Not found";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        // Get Pin Notes
        [HttpGet]
        [Route("Pin")]
        public IActionResult GetPinnedNotes()
        {
            try
            {
                bool success = false;
                string message;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(userId => userId.Type.Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);
                List<UserNoteResponseData> userNoteResponseDataList = noteBL.GetPinnedNotes(userId);

                if (userNoteResponseDataList != null)
                {
                    return Ok(userNoteResponseDataList.ToList());
                }
                else
                {
                    message = "Not found";
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
