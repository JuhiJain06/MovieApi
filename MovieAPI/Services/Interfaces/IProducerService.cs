using MovieAPI.Models.DatabaseModels;
using MovieAPI.Models.RequestModels;
using MovieAPI.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAPI.Services.Interfaces
{
    public interface IProducerService
    {
        Task<int> Add(ProducerRequestModel request);
        Task Delete(int id);
        Task<List<ProducerResponseModel>> Get();
        Task<ProducerResponseModel> Get(int id);
        Task Update(int id, ProducerUpdateRequest request);
    }
}
