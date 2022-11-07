namespace MovieDatabase.Web.Areas.Admin.Models.Screenwriters
{
    using System.ComponentModel.DataAnnotations;

    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class CreateScreenwriterVM : IMapFrom<Screenwriter>
    {
        [Required]
        public string Name { get; set; }
    }
}