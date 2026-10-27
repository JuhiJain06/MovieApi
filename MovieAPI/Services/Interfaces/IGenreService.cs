using MovieAPI.Models.DatabaseModels;
using MovieAPI.Models.RequestModels;
using MovieAPI.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAPI.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreResponseModel>> Get(string ids);
        Task<List<GenreResponseModel>> Get();
        Task<GenreResponseModel> Get(int id);
        Task<int> Add(GenreRequestModel request);
        Task Delete(int id);
        Task Update(int id,GenreUpdateRequest request);
    }
}
