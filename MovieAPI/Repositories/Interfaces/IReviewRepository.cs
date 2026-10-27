using MovieAPI.Models.DatabaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAPI.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> Get(int movieId);
        Task<Review> Get(int id, int movieId);
        Task<int> Add(Review review);
        Task Delete(Review review);
        Task Update(Review review);
        
    }
}
