namespace MovieDatabase.Web.Areas.Admin.Models.MovieCategories
{
    using System;

    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class MovieCategoryVM : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }
       
        public DateTime DeletedOn { get; set; }
    }
}