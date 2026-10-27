using MovieAPI.Models.DatabaseModels;
using MovieAPI.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAPI.Repositories.Interfaces
{
    public interface IActorsRepository
    {
        Task<IEnumerable<ActorResponseModel>> Get(List<int> ids);
        Task<IEnumerable<Actor>> Get();
        Task<Actor> Get(int id);
        Task<int> Add(Actor actor);
        Task Delete(Actor actor);
        Task Update(Actor actor);

    }
}
