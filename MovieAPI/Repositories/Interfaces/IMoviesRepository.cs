using MovieAPI.Models.DatabaseModels;
using MovieAPI.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAPI.Repositories.Interfaces
{
    public interface IMoviesRepository
    {
        Task<IEnumerable<MovieDTO>> Get();
        Task<MovieDTO> Get(int id);
        Task<int> Add(MovieDTO movie);
        Task Delete(int id);
        Task Update(MovieDTO movie);
    }
}
