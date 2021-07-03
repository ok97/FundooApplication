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

namespace FundooApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        INoteBL noteBL;
        public NotesController(INoteBL noteBl)
        {
            this.noteBL = noteBl;
        }
      //[Authorize]
        [HttpPost("Add Note")]
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
                    message = $"Create Note Failed";
                    return Ok(new { success, message });
                }
                else
                {
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

        [HttpGet("Get Notes")]
        public ActionResult GetAllNotes()
        {
            var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            int UserId = Convert.ToInt32(idClaim.Value);         
            var result = noteBL.GetAllNotes(UserId);
            if (result != null)
            {
                return this.Ok(new { success = true, message = $"List Of Notes with UserId: {UserId}.", data = result });
            }
            return BadRequest(new { success = false, message = $"No such Notes Exist." });
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
                    message = "Note Deleted Successfull";
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


        // Update Note

        [HttpPut("Update")]        
        public IActionResult UpdateNotes(int noteID, UpdateNoteRequest updateNoteRequest)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Convert.ToInt32(idClaim.Value);
                UserNoteResponseData userUpdateData = noteBL.UpdateNote(UserId, noteID, updateNoteRequest);
                bool success = false;
                string message;
                if (userUpdateData == null)
                {
                    message = $"Update Failed";
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
        public ActionResult UpdateArchived(int noteId, AddArchived UpdateArchived)
        {
            try
            {
                this.noteBL.UpdateArchived(noteId, UpdateArchived.Archived);
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
        // Put Note in Trash
        [HttpPut]
        [Route("{noteID}/Trash")]
        public IActionResult TrashNote(int noteID)
        {
            try
            {
                bool success = false, data;
                string message;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Convert.ToInt32(idClaim.Value);

                data = noteBL.TrashNote(userId, noteID);

                if (data)
                {
                    success = true;
                    message = "Note Successfully Trashed";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "Note Trash Unsuccessfull, Try again!";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        //Put Archive Note
        [HttpPost]
        [Route("{noteID}/Archieve")]
        public IActionResult ArchievedNote(int noteID)
        {
            try
            {
                bool success = false, data;
                string message;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int userID = Convert.ToInt32(idClaim.Value);

                data = noteBL.ArchievedUnarchievedNote(userID, noteID);

                if (data)
                {
                    success = true;
                    message = "Note Successfully Archieved";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "Note Archieved Unsuccessfull, Try again!";
                    return Ok(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        //Put Pin Note
        [HttpPost]
        [Route("{noteID}/Pin")]
        public IActionResult PinNote(int noteID)
        {
            try
            {
                bool success = false, data;
                string message;
                var idClaim = HttpContext.User.Claims.FirstOrDefault(UserId => UserId.Type.Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int userID = Convert.ToInt32(idClaim.Value);

                data = noteBL.PinUnpinNote(userID, noteID);

                if (data)
                {
                    success = true;
                    message = "Note Successfully Pinned";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "Note pinned Unsuccessfull, Try again!";
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
