namespace MovieDatabase.Web.Areas.Admin.Models.Screenwriters
{
    using System.Collections.Generic;

    using MovieDatabase.Data.Models;
    using MovieDatabase.Web.Areas.Admin.Models.Base;

    public class ScreenwriterListVM : PaginatedWithMappingVM<Screenwriter>
    {
        public List<ScreenwriterVM> Screenwriters { get; set; }
    }
}