using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBL
    {
        Task AddLabel(int userId, int noteId,string labelName);
        Task DeleteLabel(int userid, int noteid);
    }
}
