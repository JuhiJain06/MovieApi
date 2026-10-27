using MovieAPI.Models.DatabaseModels;
using MovieAPI.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAPI.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        Task<IEnumerable<GenreResponseModel>> Get(List<int> ids);
        Task<IEnumerable<Genre>> Get();
        Task<Genre> Get(int id);
        Task<int> Add(Genre genre);
        Task Delete(Genre genre);
        Task Update(Genre genre);
    }
}
