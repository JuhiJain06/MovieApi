using MovieAPI.Models.DatabaseModels;

namespace MovieAPI.Helpers.Interface
{
    public interface IJwtTokenGenerator
    {
        string Token(User user);
    }
}