using System.Threading.Tasks;

namespace ViewModel
{
    public interface ISaveOpen
    {
        Task OpenListUserAsync(string path);
        Task SaveListUserAsync(string path);
    }
}
