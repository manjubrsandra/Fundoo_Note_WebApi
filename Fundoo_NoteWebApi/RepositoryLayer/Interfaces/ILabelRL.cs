
using DatabaseLayer.Label;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRL
    {
        Task AddLabel(int userId,int noteId,string labelName);
        Task DeleteLabel(int userId, int noteId);
        Task UpdateLabel(int userId, int noteId, string labelName);
        Task<Label> GetLabel(int userId, int noteId);

        Task<List<LabelResponseModel>> GetAllLabel(int userId);
    }
}
