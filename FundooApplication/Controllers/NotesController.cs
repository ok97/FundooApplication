using BusinessLayer.Interfaces;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [AllowAnonymous]
        [HttpPost("AddNotes")]
        public ActionResult AddNote(AddNote note)
        {
            try
            {
                this.noteBL.AddNote(note);
                return this.Ok(new { success = true, message = $"Notes Successfully Added with UserId: {note.UserId}." });
            }
            catch (Exception)
            {
                return BadRequest(new { success = false, message = $"UserId Not Exist." });
            }

        }


        // Get Notes using UserId

        [AllowAnonymous]
        [HttpGet("{UserId}")]
        public ActionResult GetAllNotes(int UserId)
        {
            var result = this.noteBL.GetAllNotes(UserId);
            if (result != null) return this.Ok(new { success = true, message = $"List Of Notes with UserId: {UserId}.", data = result });
            return BadRequest(new { success = false, message = $"No such UserID Exist." });
        }



        // Delete Note Using NotesId

        [HttpDelete]
        [Route("{NotesId}")]
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


        
    }
}
