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
    using MovieDatabase.Web.Areas.Admin.Models.Users;

    public class UsersController : EntityListController
    {
        private ApplicationUserManager<ApplicationUser> userManager;
        private ICrudService<Post> postService;

        public UsersController(ApplicationUserManager<ApplicationUser> userManager, ICrudService<Post> postService)
        {
            this.userManager = userManager;
            this.postService = postService;
        }

        [HttpGet]
        [Route("admin/users")]
        public IActionResult Index(PaginationVM pagination, string usernameEmail)
        {
            string currentId = this.userManager.GetUserId(this.User);
            var usersQuery = this.userManager.Users.Where(u => u.Id != currentId);

            if (!string.IsNullOrWhiteSpace(usernameEmail))
            {
                usersQuery = usersQuery.Where(u => u.UserName.ToLower().Contains(usernameEmail.ToLower()) || u.Email.ToLower().Contains(usernameEmail.ToLower()));
            }

            usersQuery = usersQuery.OrderBy(u => u.IsDeleted).ThenByDescending(u => u.CreatedOn);

            var paginatedUsers = this.PaginateList<UserVM>(pagination, usersQuery.ProjectTo<UserVM>()).ToList();

            int totalPages = this.GetTotalPages(pagination.PageSize, usersQuery.Count());

            return this.View(new UserListVM
            {
                Users = paginatedUsers,
                NextPage = pagination.Page < totalPages ? pagination.Page + 1 : pagination.Page,
                PreviousPage = pagination.Page > 1 ? pagination.Page - 1 : pagination.Page,
                CurrentPage = pagination.Page,
                TotalPages = totalPages,
                ShowPagination = totalPages > 1,
            });
        }

        [HttpGet]
        [Route("admin/user/{userId}")]
        public IActionResult UserProfile(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return this.NotFound($"invalid user id");
            }

            var user = this.userManager.Users.Where(u => u.Id == userId).ProjectTo<UserVM>().FirstOrDefault();

            if (user == null)
            {
                return this.NotFound($"user not found");
            }

            if (this.HasAlert)
            {
                this.SetAlertModel();
            }

            return this.View(user);
        }

        [HttpPost]
        [Route("admin/user/{userId}/activate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return this.NotFound($"invalid user id");
            }

            IdentityResult result = await this.userManager.ActivateUserAsync(userId);

            await this.postService.Restore(this.postService.GetAllWithDeleted().Where(p => p.UserId == userId));

            this.AddAlert(true, "User account successfully activated");

            return this.RedirectToAction("UserProfile", "Users", new { userId });
        }

        [HttpPost]
        [Route("admin/user/{userId}/deactivate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeactivateUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return this.NotFound($"invalid user id");
            }

            IdentityResult result = await this.userManager.DeactivateUserAsync(userId);

            await this.postService.Delete(this.postService.GetAllWithDeleted().Where(p => p.UserId == userId));

            this.AddAlert(true, "User account successfully deactivated");

            return this.RedirectToAction("UserProfile", "Users", new { userId });
        }
    }
}