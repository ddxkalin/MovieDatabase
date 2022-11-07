namespace MovieDatabase.Web.Areas.Identity.Pages.Account.Manage
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Contracts;
    using MovieDatabase.Services.Identity;
    using MovieDatabase.Web.Areas.Identity.Pages.Account.Manage.InputModels;
    using MovieDatabase.Web.Areas.Identity.Pages.Account.Manage.OutputModels;

#pragma warning disable SA1649 // File name should match first type name
    public class ManagePostsModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        private readonly ApplicationUserManager<ApplicationUser> userManager;
        private readonly ICrudService<Post> postService;

        public ManagePostsModel(
            ApplicationUserManager<ApplicationUser> userManager,
            ICrudService<Post> postService)
        {
            this.userManager = userManager;
            this.postService = postService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public List<ManagePostsOutputModel> Output { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            var posts = this.postService.GetAll().Where(p => p.UserId == user.Id).ProjectTo<ManagePostsOutputModel>().OrderByDescending(p => p.CreatedOn).ToList();

            this.Output = posts;

            return this.Page();
        }

        public async Task<IActionResult> OnPostDeletePostAsync(string postId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            var post = await this.postService.Get(postId);

            if (post != null && post.UserId == user.Id)
            {
                await this.postService.Delete(postId);
            }

            this.StatusMessage = "The post was deleted";
            return this.RedirectToPage();
        }
    }
}
