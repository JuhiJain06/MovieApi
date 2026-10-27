using MovieAPI.Models.DatabaseModels;
using MovieAPI.Models.RequestModels;
using MovieAPI.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAPI.Services.Interfaces
{
    public interface IMovieService
    {
        Task<List<MovieResponseModel>> Get();
        Task<MovieResponseModel> Get(int id);
        Task<int> Add(MovieRequestModel request);
        Task Delete(int id);
        Task Update(int id,MovieUpdateRequest request);
    }
}
