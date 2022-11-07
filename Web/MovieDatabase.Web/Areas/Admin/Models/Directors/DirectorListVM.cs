namespace MovieDatabase.Web.Areas.Admin.Models.Directors
{
    using System.Collections.Generic;

    using MovieDatabase.Data.Models;
    using MovieDatabase.Web.Areas.Admin.Models.Base;

    public class DirectorListVM : PaginatedWithMappingVM<Director>
    {
        public List<DirectorVM> Directors { get; set; }
    }
}