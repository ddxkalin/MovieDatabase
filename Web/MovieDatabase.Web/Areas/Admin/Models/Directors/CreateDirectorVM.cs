namespace MovieDatabase.Web.Areas.Admin.Models.Directors
{
    using System.ComponentModel.DataAnnotations;

    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class CreateDirectorVM : IMapFrom<Director>
    {
        [Required]
        public string Name { get; set; }
    }
}