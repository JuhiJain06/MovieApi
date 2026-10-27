using MovieAPI.Models.DatabaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAPI.Repositories.Interfaces
{
    public interface IProducerRepository
    {

        Task<int> Add(Producer producer);
        Task Delete(Producer producer);
        Task<IEnumerable<Producer>> Get();
        Task<Producer> Get(int id);
        Task Update(Producer producer);
    }
}
