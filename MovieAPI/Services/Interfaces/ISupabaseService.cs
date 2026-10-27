using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MovieAPI.Services.Interfaces
{
    public interface ISupabaseService
    {
        Task<string> UploadPosterAsync(IFormFile file, string movieName,int year);
    }
}