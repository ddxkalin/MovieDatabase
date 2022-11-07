// ReSharper disable VirtualMemberCallInConstructor

namespace MovieDatabase.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using MovieDatabase.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public bool IsActive { get; set; }

        public DateTimeOffset EmailConfirmationTokenResentOn { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; } = new HashSet<IdentityUserRole<string>>();

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; } = new HashSet<IdentityUserClaim<string>>();

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; } = new List<IdentityUserLogin<string>>();

        public virtual ICollection<UserWishlist> UserWishlists { get; set; } = new HashSet<UserWishlist>();
        
        public virtual ICollection<UserWatchedMovie> UserWatchedMovies { get; set; } = new HashSet<UserWatchedMovie>();
        
        public virtual ICollection<UserOwnedMovie> UserOwnedMovies { get; set; } = new HashSet<UserOwnedMovie>();

        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public virtual ICollection<EventParticipant> Events { get; set; } = new List<EventParticipant>();

        public virtual ICollection<UserRating> UserRatings { get; set; } = new HashSet<UserRating>();
    }
}