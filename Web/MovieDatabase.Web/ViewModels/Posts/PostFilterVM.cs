namespace MovieDatabase.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class PostFilterVM
    {
        public string OrderBy { get; set; }

        public string MovieName { get; set; }

        public string MovieCategory { get; set; }

        public string MovieDirector { get; set; }

        public string MovieScreenwriter { get; set; }

        public string MovieComposer { get; set; }

        public string Keyword { get; set; }

        public int RatingAbove { get; set; }
    }
}