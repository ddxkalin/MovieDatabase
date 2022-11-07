namespace MovieDatabase.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using MovieDatabase.Common;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Contracts;
    using MovieDatabase.Services.Identity;
    using MovieDatabase.Web.Areas.Admin.Controllers.Base;
    using MovieDatabase.Web.Areas.Admin.Models;
    using MovieDatabase.Web.Areas.Admin.Models.Posts;

    public class PostsController : EntityListController
    {
        private ICrudService<Post> postService;

        public PostsController(ICrudService<Post> postService)
        {
            this.postService = postService;
        }

        [HttpGet]
        [Route("admin/posts")]
        public IActionResult Index(PaginationVM pagination, string movieName)
        {
            if (this.HasAlert)
            {
                this.SetAlertModel();
            }

            var postsQuery = this.postService.GetAllWithDeleted();

            if (!string.IsNullOrWhiteSpace(movieName))
            {
                postsQuery = postsQuery.Where(u => u.Movie.Name.ToLower().Contains(movieName.ToLower()));
            }

            postsQuery = postsQuery.OrderBy(u => u.IsDeleted).ThenByDescending(u => u.CreatedOn);

            var paginatedPosts = this.PaginateList<PostVM>(pagination, postsQuery.ProjectTo<PostVM>()).ToList();

            int totalPages = this.GetTotalPages(pagination.PageSize, postsQuery.Count());

            return this.View(new PostListVM
            {
                Posts = paginatedPosts,
                NextPage = pagination.Page < totalPages ? pagination.Page + 1 : pagination.Page,
                PreviousPage = pagination.Page > 1 ? pagination.Page - 1 : pagination.Page,
                CurrentPage = pagination.Page,
                TotalPages = totalPages,
                ShowPagination = totalPages > 1,
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/posts/delete")]
        public async Task<IActionResult> Delete(string postId)
        {
            if (string.IsNullOrWhiteSpace(postId))
            {
                return this.BadRequest($"invalid post id");
            }

            PaginationVM pagination = this.GetCurrentPagination();

            await this.postService.Delete(postId);

            this.AddAlert(true, "Successfully deleted post");

            return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, movieName = this.Request.Query["movieName"] });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/posts/restore")]
        public async Task<IActionResult> Restore(string postId)
        {
            if (string.IsNullOrWhiteSpace(postId))
            {
                return this.BadRequest($"invalid post id");
            }

            PaginationVM pagination = this.GetCurrentPagination();

            await this.postService.Restore(postId);

            this.AddAlert(true, "Successfully restored post");

            return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, movieName = this.Request.Query["movieName"] });
        }
    }
}