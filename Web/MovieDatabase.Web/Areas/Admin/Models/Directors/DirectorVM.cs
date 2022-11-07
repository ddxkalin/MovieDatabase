namespace MovieDatabase.Web.Areas.Admin.Models.Directors
{
    using System;

    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class DirectorVM : IMapFrom<Director>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DeletedOn { get; set; }
    }
}