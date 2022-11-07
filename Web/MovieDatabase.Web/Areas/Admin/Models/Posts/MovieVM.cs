namespace MovieDatabase.Web.Areas.Admin.Models.Posts
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class MovieVM : IMapFrom<Movie>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }
    }
}