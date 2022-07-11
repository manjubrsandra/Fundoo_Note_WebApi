using DatabaseLayer.Label;
using RepositoryLayer.Services.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBL
    {
        Task AddLabel(int userId, int noteId,string labelName);
        Task DeleteLabel(int userId, int noteId);
        Task UpdateLabel(int userId, int noteId, string labelName);
        Task<Label> GetLabel(int userId, int noteId);

        Task<List<LabelResponseModel>> GetAllLabelsByLinqJoins(int UserId);
    }
}
