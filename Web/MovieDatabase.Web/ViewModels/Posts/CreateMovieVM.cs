namespace MovieDatabase.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class CreateMovieVM : IMapFrom<Movie>
    {
        [Required(ErrorMessage = "Field is required")]
        public string Name { get; set; }
        
        public List<MovieCategoryVM> Categories { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Studio { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [MaxLength(250, ErrorMessage = "Description must be shorter than 250 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Field is required")]
        //[FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Allowed extensions are: jpg, jpeg and png")]
        public IFormFile PosterImage { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string TrailerLink { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Duration { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Language { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public List<MovieActorVM> Actors { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string DirectorId { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string ScreenwriterId { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string ComposerId { get; set; }

        public List<MovieAwardVM> Awards { get; set; }

        public double ImdbRating { get; set; }

        public RatingVM Rating { get; set; }

        public List<MovieKeywordVM> Keywords { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string KeywordsString { get; set; }
    }
}
