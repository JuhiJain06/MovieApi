using MovieAPI.Models.DatabaseModels;
using MovieAPI.Models.RequestModels;
using MovieAPI.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAPI.Services.Interfaces
{
    public interface IActorService
    {
        Task<IEnumerable<ActorResponseModel>> Get(string ids);
        Task<List<ActorResponseModel>> Get();
        Task<ActorResponseModel> Get(int id);
        Task<int> Add(ActorRequestModel request);
        Task Delete(int id);
        Task Update(int id, ActorUpdateRequest request);
    }
}
