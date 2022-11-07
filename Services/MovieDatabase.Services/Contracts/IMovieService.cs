namespace MovieDatabase.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MovieDatabase.Data.Models;

    public interface IMovieService : ICrudService<Movie>
    {
        Task<bool> AddAward(Movie entity, string awardName);

        Task<bool> AddAwards(Movie entity, List<string> awardNames);

        Task<bool> AddActor(Movie entity, string actorId);

        Task<bool> AddActors(Movie entity, List<string> actorIds);

        Task<bool> AddComposer(Movie entity, string composerId);

        Task<bool> AddScreenwriter(Movie entity, string screenwriterId);

        Task<bool> AddDirector(Movie entity, string directorId);

        Task<bool> AddRating(Movie entity, string ratingId);

        Task<bool> AddComment(Movie entity, string commentId);

        Task<bool> AddKeyword(Movie entity, string keywordId);
    }
}