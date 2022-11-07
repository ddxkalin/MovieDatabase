namespace MovieDatabase.Data.Models
{
    public class EventParticipant
    {
        public string UserId { get; set; }

        public ApplicationUser Participant { get; set; }

        public string EventId { get; set; }

        public Event Event { get; set; }
    }
}