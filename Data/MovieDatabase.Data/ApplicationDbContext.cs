namespace MovieDatabase.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using MovieDatabase.Data.Common.Models;
    using MovieDatabase.Data.Models;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Configuration.ProxyCreationEnabled = true;
        }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<Screenwriter> Screenwriters { get; set; }

        public DbSet<Composer> Composers { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Festival> Festivals { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Category> MovieCategories { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            ConfigureUserIdentityRelations(builder);
            ConfigureConstraints(builder);
            ConfigureRatingRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private static void ConfigureUserIdentityRelations(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserWishlist>()
                .HasKey(uw => new { uw.UserId, uw.MovieId });

            builder.Entity<UserWishlist>()
                .HasOne(uw => uw.Movie)
                .WithMany(p => p.UserWishlists)
                .HasForeignKey(uw => uw.MovieId);

            builder.Entity<UserWishlist>()
                .HasOne(uw => uw.User)
                .WithMany(c => c.UserWishlists)
                .HasForeignKey(pc => pc.UserId);

            builder.Entity<UserWatchedMovie>()
                .HasKey(uw => new { uw.UserId, uw.MovieId });

            builder.Entity<UserWatchedMovie>()
                .HasOne(uw => uw.Movie)
                .WithMany(p => p.UserWatchedMovies)
                .HasForeignKey(uw => uw.MovieId);

            builder.Entity<UserWatchedMovie>()
                .HasOne(uw => uw.User)
                .WithMany(c => c.UserWatchedMovies)
                .HasForeignKey(pc => pc.UserId);

            builder.Entity<UserOwnedMovie>()
               .HasKey(uw => new { uw.UserId, uw.MovieId });

            builder.Entity<UserOwnedMovie>()
                .HasOne(uw => uw.Movie)
                .WithMany(p => p.UserOwnedMovies)
                .HasForeignKey(uw => uw.MovieId);

            builder.Entity<UserOwnedMovie>()
                .HasOne(uw => uw.User)
                .WithMany(c => c.UserOwnedMovies)
                .HasForeignKey(pc => pc.UserId);

            builder.Entity<MovieCategory>()
                .HasKey(bc => new { bc.MovieId, bc.CategoryId });

            builder.Entity<MovieActor>()
                .HasKey(bc => new { bc.MovieId, bc.ActorId });

            builder.Entity<MovieCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(b => b.MoviesList)
                .HasForeignKey(bc => bc.CategoryId);

            builder.Entity<MovieActor>()
                .HasOne(bc => bc.Actor)
                .WithMany(b => b.StaredIn)
                .HasForeignKey(bc => bc.ActorId);

            builder.Entity<Director>()
                .HasMany(bc => bc.Directed)
                .WithOne(bc => bc.Director)
                .HasForeignKey(bc => bc.DirectorId);

            builder.Entity<Composer>()
                .HasMany(bc => bc.Composed)
                .WithOne(bc => bc.Composer)
                .HasForeignKey(bc => bc.ComposerId);

            builder.Entity<Screenwriter>()
                .HasMany(bc => bc.Written)
                .WithOne(bc => bc.Screenwriter)
                .HasForeignKey(bc => bc.ScreenwriterId);

            builder.Entity<MovieCategory>()
                .HasOne(bc => bc.Movie)
                .WithMany(c => c.Categories)
                .HasForeignKey(bc => bc.MovieId);

            builder.Entity<MovieActor>()
                .HasOne(bc => bc.Movie)
                .WithMany(c => c.Actors)
                .HasForeignKey(bc => bc.MovieId);

            builder.Entity<EventParticipant>()
                .HasKey(bc => new { bc.UserId, bc.EventId });

            builder.Entity<EventParticipant>()
                .HasOne(bc => bc.Participant)
                .WithMany(b => b.Events)
                .HasForeignKey(bc => bc.EventId);

            builder.Entity<EventParticipant>()
                .HasOne(bc => bc.Event)
                .WithMany(c => c.Participants)
                .HasForeignKey(bc => bc.UserId);
        }

        private static void ConfigureConstraints(ModelBuilder builder)
        {
            builder.Entity<Movie>()
                .HasIndex(u => u.Slug)
                .IsUnique();

            builder.Entity<Category>()
                .HasIndex(u => u.Slug)
                .IsUnique();
        }

        private static void ConfigureRatingRelations(ModelBuilder builder)
        {
            builder.Entity<UserRating>()
                .HasKey(uw => new { uw.UserId, uw.RatingId });

            builder.Entity<UserRating>()
                .HasOne(uw => uw.User)
                .WithMany(p => p.UserRatings)
                .HasForeignKey(uw => uw.UserId);

            builder.Entity<UserRating>()
                .HasOne(uw => uw.Rating)
                .WithMany(c => c.UserRatings)
                .HasForeignKey(pc => pc.RatingId);

            builder.Entity<MovieRating>()
                .HasKey(uw => new { uw.RatingId, uw.MovieId });

            builder.Entity<MovieRating>()
                .HasOne(uw => uw.Movie)
                .WithMany(p => p.Ratings)
                .HasForeignKey(uw => uw.MovieId);

            builder.Entity<MovieRating>()
                .HasOne(uw => uw.Rating)
                .WithMany(c => c.MovieRatings)
                .HasForeignKey(pc => pc.RatingId);
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}