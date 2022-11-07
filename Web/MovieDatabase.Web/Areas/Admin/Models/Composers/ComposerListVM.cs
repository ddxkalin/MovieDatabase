namespace MovieDatabase.Web.Areas.Admin.Models.Composers
{
    using System.Collections.Generic;

    using MovieDatabase.Data.Models;
    using MovieDatabase.Web.Areas.Admin.Models.Base;

    public class ComposerListVM : PaginatedWithMappingVM<Composer>
    {
        public List<ComposerVM> Composers { get; set; }
    }
}