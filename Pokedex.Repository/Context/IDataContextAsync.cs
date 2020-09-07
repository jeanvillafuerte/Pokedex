using System.Threading.Tasks;

namespace Pokedex.Repository
{
    public interface IDataContextAsync : IDataContext
    {
        Task<int> SaveChangesAsync();
    }
}