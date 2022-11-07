namespace MovieDatabase.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class MovieAwardVM : IMapFrom<Award>
    {
        public string Name { get; set; }
    }
}
