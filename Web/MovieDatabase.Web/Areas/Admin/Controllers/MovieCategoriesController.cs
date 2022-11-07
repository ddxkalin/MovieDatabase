namespace MovieDatabase.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MovieDatabase.Common;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Contracts;
    using MovieDatabase.Services.Utils;
    using MovieDatabase.Web.Areas.Admin.Controllers.Base;
    using MovieDatabase.Web.Areas.Admin.Models;
    using MovieDatabase.Web.Areas.Admin.Models.MovieCategories;

    public class MovieCategoriesController : EntityListController
    {
        private ICrudService<Category> movieCategoryService;
        private ICrudService<Post> postService;

        public MovieCategoriesController(ICrudService<Category> movieCategoryService, ICrudService<Post> postService)
        {
            this.movieCategoryService = movieCategoryService;
            this.postService = postService;
        }

        [HttpGet]
        [Route("admin/movieCategories")]
        public IActionResult Index(PaginationVM pagination, string name)
        {
            if (this.HasAlert)
            {
                this.SetAlertModel();
            }

            var movieCategoriesQuery = this.movieCategoryService.GetAllWithDeleted();

            if (!string.IsNullOrWhiteSpace(name))
            {
                movieCategoriesQuery = movieCategoriesQuery.Where(a => a.Name.ToLower().Contains(name.ToLower()));
            }

            movieCategoriesQuery = movieCategoriesQuery.OrderBy(u => u.IsDeleted).ThenByDescending(u => u.Name);

            var paginatedMovieCategories = this.PaginateList<MovieCategoryVM>(pagination, movieCategoriesQuery.ProjectTo<MovieCategoryVM>()).ToList();

            int totalPages = this.GetTotalPages(pagination.PageSize, movieCategoriesQuery.Count());

            MovieCategoryListVM movieCategoryListModel = new MovieCategoryListVM
            {
                MovieCategories = paginatedMovieCategories,
                NextPage = pagination.Page < totalPages ? pagination.Page + 1 : pagination.Page,
                PreviousPage = pagination.Page > 1 ? pagination.Page - 1 : pagination.Page,
                CurrentPage = pagination.Page,
                TotalPages = totalPages,
                ShowPagination = totalPages > 1,
            };

            return this.View(new MovieCategoryCombinedVM { MovieCategoryList = movieCategoryListModel });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/movieCategories/create")]
        public async Task<IActionResult> Create(MovieCategoryCombinedVM vm)
        {
            PaginationVM pagination = this.GetCurrentPagination();

            if (!this.ModelState.IsValid)
            {
                this.AddAlert(false, "An error has occured");
                return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, name = this.Request.Query["name"] });
            }

            var movieCategory = Mapper.Map<Category>(vm.MovieCategory);
            movieCategory.Slug = SlugGenerator.GenerateSlug(movieCategory.Name);

            try
            {
                await this.movieCategoryService.Create(movieCategory);
            }
            catch (DbUpdateException e)
            when (e.InnerException is SqlException sqlEx &&
                (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                this.AddAlert(false, "Cannot insert duplicate category");
                return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, name = this.Request.Query["name"] });
            }

            this.AddAlert(true, "Successfully added movie category");

            return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, name = this.Request.Query["name"] });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/movieCategories/delete")]
        public async Task<IActionResult> Delete(string movieCategoryId)
        {
            if (string.IsNullOrWhiteSpace(movieCategoryId))
            {
                return this.BadRequest($"invalid movieCategory id");
            }

            PaginationVM pagination = this.GetCurrentPagination();

            await this.movieCategoryService.Delete(movieCategoryId);

            await this.postService.Delete(this.postService.GetAllWithDeleted().Where(p => p.Movie.Categories.First().CategoryId == movieCategoryId));

            this.AddAlert(true, "Successfully deleted movie category");

            return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, name = this.Request.Query["name"] });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/movieCategories/restore")]
        public async Task<IActionResult> Restore(string movieCategoryId)
        {
            if (string.IsNullOrWhiteSpace(movieCategoryId))
            {
                return this.BadRequest($"invalid movie category id");
            }

            PaginationVM pagination = this.GetCurrentPagination();

            await this.movieCategoryService.Restore(movieCategoryId);

            await this.postService.Restore(this.postService.GetAllWithDeleted().Where(p => p.Movie.Categories.First().CategoryId == movieCategoryId));

            this.AddAlert(true, "Successfully restored movie category");

            return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, name = this.Request.Query["name"] });
        }
    }
}