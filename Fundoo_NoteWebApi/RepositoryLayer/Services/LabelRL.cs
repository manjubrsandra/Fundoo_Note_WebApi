using DatabaseLayer.Label;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class LabelRL : ILabelRL
    {
        FundooContext fundooContext;
        IConfiguration configuration;
        public LabelRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;

        }
        public async Task AddLabel(int userId, int noteId, string labelName)
        {
            try
            {
                var label1 = await fundooContext.Label.Where(c => c.UserId == userId && c.NoteId == noteId).FirstOrDefaultAsync();
                if (label1 == null)
                {

                    Label label = new Label();
                  
                        label.UserId = userId;
                        label.NoteId = noteId;
                        label.LabelName = labelName;

                    await fundooContext.Label.AddAsync(label);
                    await fundooContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteLabel(int userId, int noteId)
        {
            try
            {
                var label = fundooContext.Label.Where(u => u.UserId == userId && u.NoteId == noteId).FirstOrDefault();
                if (label != null)
                {
                    fundooContext.Label.Remove(label);
                    await fundooContext.SaveChangesAsync();
                }

            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<List<LabelResponseModel>> GetAllLabel(int userId)
        {
            try
            {
                var label = fundooContext.Label.FirstOrDefault(u => u.UserId == userId);
                if (label== null)
                {
                    return null;
                }
               // return await fundooContext.Label.ToListAsync();

                // get all label bu join linq

                return await fundooContext.Label
                    .Where(c => c.UserId == userId)
                    .Join(fundooContext.Notes
                    .Where(b => b.noteId==label.NoteId),
                    c => c.NoteId,
                    b => b.noteId,
                    (c,b)=>new LabelResponseModel
                    {
                        UserId=c.UserId,
                        NoteId=b.noteId,

                        Title=b.Title,
                        Description=b.Description,
                        LabelName=c.LabelName,

                    }).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Label> GetLabel(int userId, int noteId)
        {
            try
            {
                return await fundooContext.Label.FirstOrDefaultAsync(u => u.UserId == userId && u.NoteId == noteId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateLabel(int userId, int noteId, string labelName)
        {
            try
            {
                var label = fundooContext.Label.Where(u => u.UserId == userId && u.NoteId == noteId).FirstOrDefault();
                if (label != null)
                {

                    label.LabelName = labelName;
                    await fundooContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
