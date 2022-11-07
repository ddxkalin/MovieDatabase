namespace MovieDatabase.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using MovieDatabase.Data.Common.Models;

    public class Event : BaseDeletableModel<string>
    {
        public Event()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        
        public virtual ICollection<EventParticipant> Participants{ get; set; }

        public string MovieId { get; set; }

        [ForeignKey(nameof(MovieId))]
        public Movie Movie { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }
    }
}