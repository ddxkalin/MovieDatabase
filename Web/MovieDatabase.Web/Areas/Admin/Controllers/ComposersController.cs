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
    using MovieDatabase.Web.Areas.Admin.Models.Composers;

    public class ComposersController : EntityListController
    {
        private ICrudService<Composer> composerService;

        public ComposersController(ICrudService<Composer> composerService)
        {
            this.composerService = composerService;
        }

        [HttpGet]
        [Route("admin/composers")]
        public IActionResult Index(PaginationVM pagination, string name)
        {
            if (this.HasAlert)
            {
                this.SetAlertModel();
            }

            var composersQuery = this.composerService.GetAllWithDeleted();

            if (!string.IsNullOrWhiteSpace(name))
            {
                composersQuery = composersQuery.Where(a => a.Name.ToLower().Contains(name.ToLower()));
            }

            composersQuery = composersQuery.OrderBy(u => u.IsDeleted).ThenByDescending(u => u.Name);

            var paginatedComposers = this.PaginateList<ComposerVM>(pagination, composersQuery.ProjectTo<ComposerVM>()).ToList();

            int totalPages = this.GetTotalPages(pagination.PageSize, composersQuery.Count());

            ComposerListVM composerListModel = new ComposerListVM
            {
                Composers = paginatedComposers,
                NextPage = pagination.Page < totalPages ? pagination.Page + 1 : pagination.Page,
                PreviousPage = pagination.Page > 1 ? pagination.Page - 1 : pagination.Page,
                CurrentPage = pagination.Page,
                TotalPages = totalPages,
                ShowPagination = totalPages > 1,
            };

            return this.View(new ComposerCombinedVM { ComposerList = composerListModel });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/composers/create")]
        public async Task<IActionResult> Create(ComposerCombinedVM vm)
        {
            PaginationVM pagination = this.GetCurrentPagination();

            if (!this.ModelState.IsValid)
            {
                this.AddAlert(false, "An error has occured");
                return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, name = this.Request.Query["name"] });
            }

            var composer = Mapper.Map<Composer>(vm.Composer);

            await this.composerService.Create(composer);

            this.AddAlert(true, "Successfully added composer");

            return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, name = this.Request.Query["name"] });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/composers/delete")]
        public async Task<IActionResult> Delete(string composerId)
        {
            if (string.IsNullOrWhiteSpace(composerId))
            {
                return this.BadRequest($"invalid composer id");
            }

            PaginationVM pagination = this.GetCurrentPagination();

            await this.composerService.Delete(composerId);

            this.AddAlert(true, "Successfully deleted composer");

            return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, name = this.Request.Query["name"] });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/composers/restore")]
        public async Task<IActionResult> Restore(string composerId)
        {
            if (string.IsNullOrWhiteSpace(composerId))
            {
                return this.BadRequest($"invalid composer id");
            }

            PaginationVM pagination = this.GetCurrentPagination();

            await this.composerService.Restore(composerId);

            this.AddAlert(true, "Successfully restored composer");

            return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, name = this.Request.Query["name"] });
        }
    }
}