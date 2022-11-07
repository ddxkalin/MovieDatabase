namespace MovieDatabase.Data.Models
{
    using System;
    using MovieDatabase.Data.Common.Models;

    public class Festival : BaseDeletableModel<string>
    {
        public Festival()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Info { get; set; }

        public string Location { get; set; }

        public DateTime Date { get; set; }
    }
}