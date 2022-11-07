namespace MovieDatabase.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Contracts;
    using MovieDatabase.Services.Identity;
    using MovieDatabase.Services.Utils;
    using MovieDatabase.Web.Controllers.Base;
    using MovieDatabase.Web.ViewModels;
    using MovieDatabase.Web.ViewModels.Posts;

    public class PostsController : EntityListController
    {
        private IHostingEnvironment hostingEnvironment;
        private ApplicationUserManager<ApplicationUser> userManager;

        private ICrudService<Post> postService;
        private ICrudService<Category> movieCategoryService;
        private ICrudService<Director> directorService;
        private ICrudService<Screenwriter> screenwriterService;
        private ICrudService<Composer> composerService;
        private ICrudService<Movie> movieService;
        private ImdbService imdbService;

        public PostsController(
            ICrudService<Post> postService,
            ICrudService<Category> movieCategoryService,
            ICrudService<Director> directorService,
            ICrudService<Screenwriter> screenwriterService,
            ICrudService<Composer> composerService,
            ICrudService<Movie> movieService,
            ImdbService imdbService,
            IHostingEnvironment hostingEnvironment,
            ApplicationUserManager<ApplicationUser> userManager)
        {
            this.postService = postService;
            this.movieCategoryService = movieCategoryService;
            this.directorService = directorService;
            this.screenwriterService = screenwriterService;
            this.composerService = composerService;
            this.movieService = movieService;

            this.imdbService = imdbService;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
        }

        [Route("posts")]
        public IActionResult Index(PaginationVM pagination, PostFilterVM postFilter)
        {
            if (this.HasAlert)
            {
                this.SetAlertModel();
            }

            var postsQuery = this.FilterPosts(postFilter, this.postService.GetAll().ProjectTo<PostVM>());

            var paginatedPosts = this.PaginateList<PostVM>(pagination, postsQuery).ToList();

            foreach (var post in paginatedPosts)
            {
                post.Movie.PosterImageRelativeLink = FileManager.GetRelativeFilePath(post.Movie.PosterImageLink);
                post.Movie.OverallRating = post.Movie.Ratings.Any() ? post.Movie.Ratings.Average(s => s.Rating.Score) : 0;
            }

            int totalPages = this.GetTotalPages(pagination.PageSize, postsQuery.Count());

            PostListVM postListViewModel = new PostListVM
            {
                Posts = paginatedPosts,
                NextPage = pagination.Page < totalPages ? pagination.Page + 1 : pagination.Page,
                PreviousPage = pagination.Page > 1 ? pagination.Page - 1 : pagination.Page,
                CurrentPage = pagination.Page,
                TotalPages = totalPages,
                ShowPagination = totalPages > 1,
            };

            this.LoadListMoviesDropdowns(postFilter);

            return this.View(postListViewModel);
        }

        [HttpGet]
        [Route("posts/wishlisted")]
        public IActionResult Wishlisted(PaginationVM pagination, PostFilterVM postFilter)
        {
            if (this.HasAlert)
            {
                this.SetAlertModel();
            }

            var userId = this.userManager.GetUserId(this.User);

            var wishlistedPosts = this.postService.GetAll()
                .Include(p => p.Movie)
                .ThenInclude(m => m.UserWishlists)
                .Where(m => m.Movie.UserWishlists.Any(w => w.UserId == userId));

            var postsQuery = this.FilterPosts(postFilter, wishlistedPosts.ProjectTo<PostVM>());

            var paginatedPosts = this.PaginateList<PostVM>(pagination, postsQuery).ToList();

            foreach (var post in paginatedPosts)
            {
                post.Movie.PosterImageRelativeLink = FileManager.GetRelativeFilePath(post.Movie.PosterImageLink);
                post.Movie.OverallRating = post.Movie.Ratings.Any() ? post.Movie.Ratings.Average(s => s.Rating.Score) : 0;
            }

            int totalPages = this.GetTotalPages(pagination.PageSize, postsQuery.Count());

            PostListVM postListViewModel = new PostListVM
            {
                Posts = paginatedPosts,
                NextPage = pagination.Page < totalPages ? pagination.Page + 1 : pagination.Page,
                PreviousPage = pagination.Page > 1 ? pagination.Page - 1 : pagination.Page,
                CurrentPage = pagination.Page,
                TotalPages = totalPages,
                ShowPagination = totalPages > 1,
            };

            this.LoadListMoviesDropdowns(postFilter);

            return this.View("Index", postListViewModel);
        }

        [HttpGet]
        [Route("posts/create")]
        public IActionResult Create()
        {
            this.LoadCreateMovieDropdowns();

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("posts/create")]
        public async Task<IActionResult> Create(CreatePostVM postModel)
        {
            if (this.HasAlert)
            {
                this.SetAlertModel();
            }

            if (!this.ModelState.IsValid)
            {
                this.AddAlert(false, "An error has occured");

                this.LoadCreateMovieDropdowns();
                return this.View(postModel);
            }

            var post = Mapper.Map<Post>(postModel);

            post.UserId = this.userManager.GetUserId(this.User);

            post.Movie.Slug = SlugGenerator.GenerateSlug(post.Movie.Name);
            post.Movie.PosterImageLink = await FileManager.SaveFile(this.hostingEnvironment, postModel.Movie.PosterImage);

            var rating = Mapper.Map<Rating>(postModel.Movie.Rating);
            rating.RatedById = this.userManager.GetUserId(this.User);
            rating.RatedOn = DateTime.Now;

            post.Movie.Ratings.Add(new MovieRating { Rating = rating });

            foreach (var keyword in postModel.Movie.KeywordsString.Trim().Split(","))
            {
                post.Movie.Keywords.Add(new Keyword { Name = keyword.Trim() });
            }

            try
            {
                await this.postService.Create(post);
            }
            catch (DbUpdateException e)
            when (e.InnerException is SqlException sqlEx &&
                (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                this.AddAlert(false, "Cannot insert duplicate movie");

                this.LoadCreateMovieDropdowns();
                return this.View(postModel);
            }

            this.AddAlert(true, "Successfully added post");

            return this.RedirectToAction("Index", new PaginationVM { Page = 1, PageSize = 20 });
        }

        [Route("posts/post/{postId}")]
        public IActionResult Post(string postId)
        {
            if (this.HasAlert)
            {
                this.SetAlertModel();
            }

            var postModel = this.postService.GetAll().Where(p => p.Id == postId).ProjectTo<PostVM>().FirstOrDefault();

            if (postModel == null)
            {
                return this.NotFound("Post not found");
            }

            var userId = this.userManager.GetUserId(this.User);

            postModel.Movie.PosterImageRelativeLink = FileManager.GetRelativeFilePath(postModel.Movie.PosterImageLink);
            postModel.Movie.OverallRating = postModel.Movie.Ratings.Any() ? postModel.Movie.Ratings.Average(s => s.Rating.Score) : 0;
            postModel.Movie.IsInWishlist = postModel.Movie.UserWishlists.Any(w => w.User.Id == userId);

            var currentUserRating = postModel.Movie.Ratings.Where(r => r.Rating.RatedBy.Id == userId).LastOrDefault()?.Rating.Score;
            postModel.Movie.GivenUserRating = currentUserRating.HasValue ? currentUserRating.Value : 0;

            return this.View(postModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("posts/post/{postId}/rate")]
        public async Task<IActionResult> RatePost(string postId, int rating)
        {
            var post = this.postService.GetAll().Where(p => p.Id == postId)
                .Include(p => p.Movie)
                .ThenInclude(m => m.Ratings)
                .ThenInclude(r => r.Rating)
                .FirstOrDefault();

            if (post == null)
            {
                return this.NotFound("Post not found");
            }

            var userId = this.userManager.GetUserId(this.User);

            if (rating > 0 && rating <= 5)
            {
                var currentUserRating = post.Movie.Ratings.Where(r => r.Rating.RatedById == userId).LastOrDefault();

                if (currentUserRating == null)
                {
                    post.Movie.Ratings.Add(new MovieRating
                    {
                        MovieId = post.MovieId,
                        Rating = new Rating
                        {
                            RatedById = userId,
                            Score = rating,
                            RatedOn = DateTime.Now
                        }
                    });
                }
                else
                {
                    currentUserRating.Rating.Score = rating;
                }

                await this.postService.Update(post);

                double overallRating = post.Movie.Ratings.Any() ? post.Movie.Ratings.Average(s => s.Rating.Score) : 0;

                return this.Json(new { overallRating });
            }

            return this.BadRequest("invalid rating");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("posts/post/{postId}/add-to-wishlist")]
        public async Task<IActionResult> AddToWishlist(string postId)
        {
            var post = this.postService.GetAll().Where(p => p.Id == postId)
                .Include(p => p.Movie)
                .ThenInclude(m => m.UserWishlists)
                .FirstOrDefault();

            if (post == null)
            {
                return this.NotFound("Post not found");
            }

            var userId = this.userManager.GetUserId(this.User);

            bool hasInWishlist = post.Movie.UserWishlists.Any(w => w.UserId == userId);

            if (!hasInWishlist)
            {
                post.Movie.UserWishlists.Add(new UserWishlist { UserId = userId });
                await this.postService.Update(post);
            }

            return this.Json(new AlertVM { Success = true, Message = "Added to wishlist" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("posts/post/{postId}/remove-from-wishlist")]
        public async Task<IActionResult> RemoveFromWishlist(string postId)
        {
            var post = this.postService.GetAll().Where(p => p.Id == postId)
                .Include(p => p.Movie)
                .ThenInclude(m => m.UserWishlists)
                .FirstOrDefault();

            if (post == null)
            {
                return this.NotFound("Post not found");
            }

            var userId = this.userManager.GetUserId(this.User);

            var userWishlistEntry = post.Movie.UserWishlists.Where(w => w.UserId == userId).FirstOrDefault();

            if (userWishlistEntry != null)
            {
                post.Movie.UserWishlists.Remove(userWishlistEntry);
                await this.postService.Update(post);
            }

            return this.Json(new AlertVM { Success = true, Message = "Removed from wishlist" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("posts/post/{postId}/comment-post")]
        public async Task<IActionResult> AddComment(string postId, string commentText)
        {
            var post = this.postService.GetAll().Where(p => p.Id == postId)
                .Include(p => p.Movie)
                .ThenInclude(m => m.Comments)
                .FirstOrDefault();

            if (post == null)
            {
                return this.NotFound("Post not found");
            }

            var userId = this.userManager.GetUserId(this.User);

            if (!string.IsNullOrWhiteSpace(commentText))
            {
                var comment = new Comment { AuthorId = userId, Name = commentText };
                post.Movie.Comments.Add(comment);
                await this.postService.Update(post);

                var user = await this.userManager.GetUserAsync(this.User);
                return this.Json(new
                {
                    comment = comment.Name,
                    createdOn = comment.CreatedOn.ToString("dd/MM/yy HH:mm"),
                    author = user.Firstname + " " + user.Lastname,
                    alert = new AlertVM { Success = true, Message = "Comment posted" }
                });
            }
            else
            {
                return this.BadRequest("Invalid comment text");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("posts/get-imdb-rating")]
        public IActionResult GetMovieImdbRating(string title)
        {
            var imdbMovie = this.imdbService.GetMovieByTitle(title);

            if (imdbMovie.Response)
            {
                return this.Json(new { imdbMovie.ImdbRating });
            }

            return this.BadRequest(imdbMovie.Error);
        }

        private void LoadCreateMovieDropdowns()
        {
            this.ViewBag.MovieCategories = this.movieCategoryService.GetAll().OrderBy(e => e.Name)
                .Select(e => new SelectListItem { Text = e.Name, Value = e.Id })
                .ToList();

            this.ViewBag.Directors = this.directorService.GetAll().OrderBy(e => e.Name)
                .Select(e => new SelectListItem { Text = e.Name, Value = e.Id })
                .ToList();

            this.ViewBag.Screenwriters = this.screenwriterService.GetAll().OrderBy(e => e.Name)
                .Select(e => new SelectListItem { Text = e.Name, Value = e.Id })
                .ToList();

            this.ViewBag.Composers = this.composerService.GetAll().OrderBy(e => e.Name)
                .Select(e => new SelectListItem { Text = e.Name, Value = e.Id })
                .ToList();
        }

        private void LoadListMoviesDropdowns(PostFilterVM postFilter)
        {
            this.ViewBag.OrderBy = new List<SelectListItem>
            {
                new SelectListItem { Text = "Alphabetical", Value = "alphabetical", Selected = postFilter.OrderBy == "alphabetical" },
                new SelectListItem { Text = "Category", Value = "category", Selected = postFilter.OrderBy == "category" },
                new SelectListItem { Text = "Rating", Value = "rating", Selected = postFilter.OrderBy == "rating" },
            };

            this.ViewBag.Ratings = new List<SelectListItem>
            {
                new SelectListItem { Text = "0", Value = "0", Selected = postFilter.RatingAbove == 0 },
                new SelectListItem { Text = "1", Value = "1", Selected = postFilter.RatingAbove == 1 },
                new SelectListItem { Text = "2", Value = "2", Selected = postFilter.RatingAbove == 2 },
                new SelectListItem { Text = "3", Value = "3", Selected = postFilter.RatingAbove == 4 },
                new SelectListItem { Text = "4", Value = "4", Selected = postFilter.RatingAbove == 4 },
                new SelectListItem { Text = "5", Value = "5", Selected = postFilter.RatingAbove == 5 },
            };

            this.ViewBag.MovieCategories = this.movieCategoryService.GetAll().OrderBy(e => e.Name)
                .Select(e => new SelectListItem { Text = e.Name, Value = e.Name.ToLower(), Selected = postFilter.MovieCategory == e.Name.ToLower() })
                .ToList();

            this.ViewBag.Directors = this.directorService.GetAll().OrderBy(e => e.Name)
                .Select(e => new SelectListItem { Text = e.Name, Value = e.Name.ToLower(), Selected = postFilter.MovieDirector == e.Name.ToLower() })
                .ToList();

            this.ViewBag.Screenwriters = this.screenwriterService.GetAll().OrderBy(e => e.Name)
                .Select(e => new SelectListItem { Text = e.Name, Value = e.Name.ToLower(), Selected = postFilter.MovieScreenwriter == e.Name.ToLower() })
                .ToList();

            this.ViewBag.Composers = this.composerService.GetAll().OrderBy(e => e.Name)
                .Select(e => new SelectListItem { Text = e.Name, Value = e.Name.ToLower(), Selected = postFilter.MovieComposer == e.Name.ToLower() })
                .ToList();
        }

        private IQueryable<PostVM> FilterPosts(PostFilterVM filter, IQueryable<PostVM> query)
        {
            if (string.IsNullOrWhiteSpace(filter.OrderBy))
            {
                query = query.OrderBy(q => q.Movie.Name);
            }
            else
            {
                switch (filter.OrderBy.Trim().ToLower())
                {
                    case "alphabetical":
                        {
                            query = query.OrderBy(q => q.Movie.Name);
                        }

                        break;
                    case "rating":
                        {
                            query = query.OrderBy(q => q.Movie.Ratings.Average(s => s.Rating.Score));
                        }

                        break;
                    case "category":
                        {
                            query = query.OrderBy(q => q.Movie.Categories.First().Category.Name);
                        }

                        break;
                }
            }

            query = query.Where(q => q.Movie.Ratings.Average(m => m.Rating.Score) >= filter.RatingAbove);

            if (!string.IsNullOrWhiteSpace(filter.MovieName))
            {
                query = query.Where(q => q.Movie.Name.ToLower().Contains(filter.MovieName.ToLower().Trim()));
            }

            if (!string.IsNullOrWhiteSpace(filter.MovieCategory))
            {
                query = query.Where(q => q.Movie.Categories.First().Category.Name.ToLower() == filter.MovieCategory);
            }

            if (!string.IsNullOrWhiteSpace(filter.MovieDirector))
            {
                query = query.Where(q => q.Movie.Director.Name.ToLower() == filter.MovieDirector.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(filter.MovieComposer))
            {
                query = query.Where(q => q.Movie.Composer.Name.ToLower() == filter.MovieComposer.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(filter.MovieScreenwriter))
            {
                query = query.Where(q => q.Movie.Screenwriter.Name.ToLower() == filter.MovieScreenwriter.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(filter.Keyword))
            {
                query = query.Where(q => q.Movie.Keywords.Any(k => k.Name == filter.Keyword));
            }

            return query;
        }
    }
}
