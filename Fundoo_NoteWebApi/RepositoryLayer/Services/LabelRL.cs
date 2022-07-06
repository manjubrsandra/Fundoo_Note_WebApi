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
    }
}
