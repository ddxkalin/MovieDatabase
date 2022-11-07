namespace MovieDatabase.Data.Models
{
    public class MovieActor
    {
        public string MovieId { get; set; }

        public Movie Movie { get; set; }

        public Actor Actor { get; set; }
        
        public string ActorId { get; set; }

        public string CharacterName { get; set; }
    }
}