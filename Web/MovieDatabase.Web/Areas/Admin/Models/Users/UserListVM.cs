namespace MovieDatabase.Web.Areas.Admin.Models.Users
{
    using System.Collections.Generic;

    using MovieDatabase.Data.Models;
    using MovieDatabase.Web.Areas.Admin.Models.Base;

    public class UserListVM : PaginatedWithMappingVM<ApplicationUser>
    {
        public List<UserVM> Users { get; set; }
    }
}