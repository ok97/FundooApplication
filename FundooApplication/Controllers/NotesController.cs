using BusinessLayer.Interfaces;
using CommonLayer.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


        //

        [AllowAnonymous]
        [HttpGet("{userId}")]
        public ActionResult GetAllNotes(int userId)
        {
            var result = this.noteBL.GetAllNotes(userId);
            if (result != null) return this.Ok(new { success = true, message = $"List Of Notes with UserId: {userId}.", data = result });
            return BadRequest(new { success = false, message = $"No such UserID Exist." });
        }

    }
}
