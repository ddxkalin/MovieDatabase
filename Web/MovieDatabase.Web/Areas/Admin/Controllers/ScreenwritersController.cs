namespace MovieDatabase.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using MovieDatabase.Common;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Contracts;
    using MovieDatabase.Web.Areas.Admin.Controllers.Base;
    using MovieDatabase.Web.Areas.Admin.Models;
    using MovieDatabase.Web.Areas.Admin.Models.Screenwriters;

    public class ScreenwritersController : EntityListController
    {
        private ICrudService<Screenwriter> screenwriterService;

        public ScreenwritersController(ICrudService<Screenwriter> screenwriterService)
        {
            this.screenwriterService = screenwriterService;
        }

        [HttpGet]
        [Route("admin/screenwriters")]
        public IActionResult Index(PaginationVM pagination, string name)
        {
            if (this.HasAlert)
            {
                this.SetAlertModel();
            }

            var screenwritersQuery = this.screenwriterService.GetAllWithDeleted();

            if (!string.IsNullOrWhiteSpace(name))
            {
                screenwritersQuery = screenwritersQuery.Where(a => a.Name.ToLower().Contains(name.ToLower()));
            }

            screenwritersQuery = screenwritersQuery.OrderBy(u => u.IsDeleted).ThenByDescending(u => u.Name);

            var paginatedScreenwriters = this.PaginateList<ScreenwriterVM>(pagination, screenwritersQuery.ProjectTo<ScreenwriterVM>()).ToList();

            int totalPages = this.GetTotalPages(pagination.PageSize, screenwritersQuery.Count());

            ScreenwriterListVM screenwriterListModel = new ScreenwriterListVM
            {
                Screenwriters = paginatedScreenwriters,
                NextPage = pagination.Page < totalPages ? pagination.Page + 1 : pagination.Page,
                PreviousPage = pagination.Page > 1 ? pagination.Page - 1 : pagination.Page,
                CurrentPage = pagination.Page,
                TotalPages = totalPages,
                ShowPagination = totalPages > 1,
            };

            return this.View(new ScreenwriterCombinedVM { ScreenwriterList = screenwriterListModel });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/screenwriters/create")]
        public async Task<IActionResult> Create(ScreenwriterCombinedVM vm)
        {
            PaginationVM pagination = this.GetCurrentPagination();

            if (!this.ModelState.IsValid)
            {
                this.AddAlert(false, "An error has occured");
                return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, name = this.Request.Query["name"] });
            }

            var screenwriter = Mapper.Map<Screenwriter>(vm.Screenwriter);

            await this.screenwriterService.Create(screenwriter);

            this.AddAlert(true, "Successfully added screenwriter");

            return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, name = this.Request.Query["name"] });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/screenwriters/delete")]
        public async Task<IActionResult> Delete(string screenwriterId)
        {
            if (string.IsNullOrWhiteSpace(screenwriterId))
            {
                return this.BadRequest($"invalid screenwriter id");
            }

            PaginationVM pagination = this.GetCurrentPagination();

            await this.screenwriterService.Delete(screenwriterId);

            this.AddAlert(true, "Successfully deleted screenwriter");

            return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, name = this.Request.Query["name"] });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/screenwriters/restore")]
        public async Task<IActionResult> Restore(string screenwriterId)
        {
            if (string.IsNullOrWhiteSpace(screenwriterId))
            {
                return this.BadRequest($"invalid screenwriter id");
            }

            PaginationVM pagination = this.GetCurrentPagination();

            await this.screenwriterService.Restore(screenwriterId);

            this.AddAlert(true, "Successfully restored screenwriter");

            return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, name = this.Request.Query["name"] });
        }
    }
}