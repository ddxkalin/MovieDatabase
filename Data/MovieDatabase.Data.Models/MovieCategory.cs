namespace MovieDatabase.Data.Models
{
    public class MovieCategory
    {
        public string MovieId { get; set; }

        public Movie Movie { get; set; }

        public Category Category { get; set; }
        
        public string CategoryId { get; set; }
    }
}