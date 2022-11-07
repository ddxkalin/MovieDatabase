namespace MovieDatabase.Web.Areas.Admin.Models.Users
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class UserVM : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool IsActive { get; set; }

        //public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        //public DateTime DeletedOn { get; set; }
    }
}