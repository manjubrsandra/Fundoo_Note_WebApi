using BusinessLayer.Interfaces;
using DatabaseLayer.Label;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundoo_NoteWebApi.Controllers
{
    public class LabelController : ControllerBase
    {
        FundooContext fundooContext;
        ILabelBL labelBL;

        public LabelController(FundooContext fundooContext, ILabelBL labelBL)
        {
            this.fundooContext = fundooContext;
            this.labelBL = labelBL;

        }
        [Authorize]
        [HttpPost("AddLabel/{NoteId}/{LabelName}")]
        public async Task<ActionResult> AddLabel(int NoteId, string LabelName)
        {
            try
            {
                var currentUser = HttpContext.User;

                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                var Label = fundooContext.Label.Where(x => x.UserId == userId && x.NoteId == NoteId).FirstOrDefault();
                if (Label != null)
                {
                    return this.BadRequest(new { status = 301, isSuccess = false, Message = "Enter new noteId" });
                }
                await this.labelBL.AddLabel(userId, NoteId, LabelName);

                return this.Ok(new { status = 200, isSucces = true, Message = "Label created succesfully" });

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Authorize]
        [HttpDelete("DeleteLabel/{noteId}")]

        public async Task<ActionResult> DeleteLabel(int noteId)
        {
            try
            {
                var currentUser = HttpContext.User;
                int UserId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                var label = fundooContext.Label.FirstOrDefault(u => u.UserId == UserId && u.NoteId == noteId);
                if (label == null)
                {
                    return this.BadRequest(new { success = false, Message = "Label Doesn't Exists" });
                }
                await this.labelBL.DeleteLabel(UserId, noteId);
                return this.Ok(new { success = true, Message = "Label Deleted Successfully" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Authorize]
        [HttpPut("UpdateLabel/{noteId}")]

        public async Task<ActionResult> UpdateLabel(int noteId, string LabelName)
        {
            try
            {
                var currentUser = HttpContext.User;
                int UserId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);


                var label = fundooContext.Label.Where(u => u.UserId == UserId && u.NoteId == noteId).FirstOrDefault();
                if (label == null)
                {
                    return this.BadRequest(new { success = true, Message = "Label Doesn't Exists" });
                }
                await this.labelBL.UpdateLabel(UserId, noteId, LabelName);
                return this.Ok(new { success = true, Message = "Label Updated successfully" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Authorize]
        [HttpGet("GetLabel/{noteId}")]
        public async Task<ActionResult> GetLabel(int noteId)
        {
            try
            {
                var currentUser = HttpContext.User;
                int UserId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                var label = fundooContext.Label.FirstOrDefault(x => x.UserId == UserId && x.NoteId == noteId);
                if (label == null)
                {
                    return this.BadRequest(new { success = false, Message = "Label Doesn't Exists" });
                }

                var label1 = await this.labelBL.GetLabel(UserId, noteId);
                return this.Ok(new { success = true, Message = $"Label Obtained Successfully for {label.LabelName}", data = label1 });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Authorize]
        [HttpGet("GetAllLabel")]

        public async Task<ActionResult> GetAllLabel()
        {
            try
            {
                var currentUser = HttpContext.User;
                var UserId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type=="UserId").Value);

                var label = fundooContext.Label.FirstOrDefault(u => u.UserId==UserId);
                if (label == null)
                {
                    this.BadRequest(new { success = false, Message = "Label doesn't exist" });
                }
                List<LabelResponseModel> labelList = new List<LabelResponseModel>();
                labelList = await this.labelBL.GetAllLabel(UserId);
                return Ok(new { success = true, Message = $"Note Obtained successfully ", data = labelList });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}