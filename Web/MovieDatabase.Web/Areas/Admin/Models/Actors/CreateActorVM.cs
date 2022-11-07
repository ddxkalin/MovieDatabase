namespace MovieDatabase.Web.Areas.Admin.Models.Actors
{
    using System.ComponentModel.DataAnnotations;

    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class CreateActorVM: IMapFrom<Actor>
    {
        [Required]
        public string Name { get; set; }
    }
}