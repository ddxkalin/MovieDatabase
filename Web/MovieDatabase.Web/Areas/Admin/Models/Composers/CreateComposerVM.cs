namespace MovieDatabase.Web.Areas.Admin.Models.Composers
{
    using System.ComponentModel.DataAnnotations;

    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class CreateComposerVM : IMapFrom<Composer>
    {
        [Required]
        public string Name { get; set; }
    }
}