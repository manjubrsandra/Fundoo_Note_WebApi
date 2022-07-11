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

        public async Task<List<LabelResponseModel>> GetAllLabelsByLinqJoins(int UserId)
        {
            try
            {
                var label = fundooContext.Label.FirstOrDefault(u => u.UserId == UserId);
                if (label == null)
                {
                    return null;
                }


                var res = await(from user in fundooContext.Users
                                join notes in fundooContext.Notes on user.UserId equals UserId
                                join labels in fundooContext.Label on notes.noteId equals labels.NoteId
                                where labels.UserId == UserId


                                select new LabelResponseModel
                                {
                                    UserId = UserId,
                                    NoteId = notes.noteId,
                                    Title = notes.Title,
                                    FirstName=user.FirstName,
                                    LastName=user.LastName,
                                    Email=user.Email,
                                    Description = notes.Description,
                                    LabelName   = labels.LabelName,
                                }).ToListAsync();



                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
