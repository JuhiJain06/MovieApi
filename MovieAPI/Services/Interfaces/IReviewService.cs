using MovieAPI.Models.RequestModels;
using MovieAPI.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAPI.Services.Interfaces
{
    public interface IReviewService
    {
        Task<int> Add(int movieId, ReviewRequestModel request);
        Task Delete(int movieId, int id);
        Task<List<ReviewResponseModel>> Get(int movieId);
        Task<ReviewResponseModel> Get(int movieId, int id);
        Task Update(int movieId, int id, ReviewUpdateRequest request);
    }
}
