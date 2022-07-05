using BusinessLayer.Interfaces;
using DatabaseLayer.Note;
using DatabaseLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Migrations;
using RepositoryLayer.Services;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundoo_NoteWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase

    {

        INoteBL noteBL;
        FundooContext fundooContext;

        public NoteController(INoteBL noteBL, FundooContext fundooContext)
        {
            this.noteBL = noteBL;
            this.fundooContext = fundooContext;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddNote(NotePostModel notePostModel)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                await this.noteBL.AddNote(userId, notePostModel);
                return this.Ok(new { success = true, message = "Note Added Successfully" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Authorize]
        [HttpGet("{AllNote}")]

        public async Task<ActionResult> GetAllNote()
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                var note = fundooContext.Notes.FirstOrDefault(u => u.UserId == userId);
                if (note == null)
                {

                    return this.BadRequest(new { success = true, message = "Note Doesn't Exits" });

                }
                List<Note> noteList = new List<Note>();

                noteList =  await this.noteBL.GetAllNote(userId);

                return Ok(new { success = true, message = "GetAllNote Successfully", data = noteList });

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [Authorize]
        [HttpDelete("{NoteId}")]

        public async Task<ActionResult> DeleteNote(int NoteId)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                var note = fundooContext.Notes.FirstOrDefault(u => u.UserId == userId && u.noteID == NoteId);
                if (note == null)
                {

                    return this.BadRequest(new { success = true, message = "Sorry! Note Doesn't Exist Please Create a Notes" });

                }
                await this.noteBL.DeleteNote(userId, NoteId);

                return Ok(new { success = true, message = $"Note Deleted Successfully for the note, {note.Title} " });

            }
            catch (Exception e)
            {
                throw e;
            }
            //$"cartList fetched Fail {e.Message}"
        }

        [Authorize]
        [HttpPut("UpdateNote/{noteId}")]
        public async Task<ActionResult> UpdateNote(int noteid, NoteUpdateModel noteUpdateModel)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);

                var noteCheck = fundooContext.Notes.FirstOrDefault(u => u.UserId == userId && u.noteID == noteid);
                if (noteCheck == null)
                {
                    return this.BadRequest(new { success = true, message = "Note does't Exist" });

                }
                await this.noteBL.UpdateNote(userId,noteid,noteUpdateModel);

                return Ok (new { succes = true, message= "Update note succesfull" });
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        [Authorize]
        [HttpPut("Remainder/{NoteId}")]

        public async Task<ActionResult> RemainderNote(int NoteId)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                var note = fundooContext.Notes.FirstOrDefault(u => u.UserId == userId && u.noteID == NoteId);
                if (note == null)
                {

                    return this.BadRequest(new { success = true, message = "Sorry! Note Doesn't Exist Please Create a Notes" });

                }
                await this.noteBL.DeleteNote(userId, NoteId);

                return Ok(new { success = true, message = $"Remainder Note Successfully for the note, {note.Title} " });

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

}
