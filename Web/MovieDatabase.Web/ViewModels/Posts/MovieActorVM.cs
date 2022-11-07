namespace MovieDatabase.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class MovieActorVM : IMapFrom<MovieActor>
    {
        [Required(ErrorMessage = "Field is required")]
        public string ActorId { get; set; }

        public ActorVM Actor { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string CharacterName { get; set; }
    }
}
