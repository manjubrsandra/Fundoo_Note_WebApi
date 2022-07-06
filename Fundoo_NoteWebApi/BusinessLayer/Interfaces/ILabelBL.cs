using RepositoryLayer.Services.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBL
    {
        Task AddLabel(int userId, int noteId,string labelName);
        Task DeleteLabel(int userid, int noteid);
        Task UpdateLabel(int userId, int noteId, string labelName);
        Task<Label> GetLabel(int userId, int noteId);

        Task<List<Label>> GetAllLabel(int userId);
    }
}
