namespace MovieDatabase.Web.Areas.Admin.Models.Actors
{
    using System.Collections.Generic;

    using MovieDatabase.Data.Models;
    using MovieDatabase.Web.Areas.Admin.Models.Base;

    public class ActorListVM : PaginatedWithMappingVM<Actor>
    {
        public List<ActorVM> Actors { get; set; }
    }
}