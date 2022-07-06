using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using System;
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
        public async Task<ActionResult> AddLabel(int NoteId,string LabelName)
        {
            try
            {
                var currentUser = HttpContext.User;

                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                var Label = fundooContext.Label.Where(x => x.UserId == userId && x.NoteId == NoteId).FirstOrDefault();
                if (Label != null)
                {
                    return this.BadRequest(new {  status= 301, isSuccess=false,Message ="Enter new noteId"});
                }
                await this.labelBL.AddLabel(userId, NoteId,LabelName);

                return this.Ok(new { status= 200,  isSucces = true, Message = "Label created succesfully" });
               
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}