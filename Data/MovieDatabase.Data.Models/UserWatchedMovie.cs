namespace MovieDatabase.Data.Models
{
    public class UserWatchedMovie
    {
        public string MovieId { get; set; }

        public Movie Movie { get; set; }

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }
    }
}