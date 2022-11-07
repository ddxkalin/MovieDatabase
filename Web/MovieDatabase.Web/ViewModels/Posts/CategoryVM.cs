namespace MovieDatabase.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class CategoryVM : IMapFrom<Category>
    {
        public string Name { get; set; }
    }
}
