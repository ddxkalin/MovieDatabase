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
    using MovieDatabase.Web.Areas.Admin.Models.Directors;

    public class DirectorsController : EntityListController
    {
        private ICrudService<Director> directorService;

        public DirectorsController(ICrudService<Director> directorService)
        {
            this.directorService = directorService;
        }

        [HttpGet]
        [Route("admin/directors")]
        public IActionResult Index(PaginationVM pagination, string name)
        {
            if (this.HasAlert)
            {
                this.SetAlertModel();
            }

            var directorsQuery = this.directorService.GetAllWithDeleted();

            if (!string.IsNullOrWhiteSpace(name))
            {
                directorsQuery = directorsQuery.Where(a => a.Name.ToLower().Contains(name.ToLower()));
            }

            directorsQuery = directorsQuery.OrderBy(u => u.IsDeleted).ThenByDescending(u => u.Name);

            var paginatedDirectors = this.PaginateList<DirectorVM>(pagination, directorsQuery.ProjectTo<DirectorVM>()).ToList();

            int totalPages = this.GetTotalPages(pagination.PageSize, directorsQuery.Count());

            DirectorListVM directorListModel = new DirectorListVM
            {
                Directors = paginatedDirectors,
                NextPage = pagination.Page < totalPages ? pagination.Page + 1 : pagination.Page,
                PreviousPage = pagination.Page > 1 ? pagination.Page - 1 : pagination.Page,
                CurrentPage = pagination.Page,
                TotalPages = totalPages,
                ShowPagination = totalPages > 1,
            };

            return this.View(new DirectorCombinedVM { DirectorList = directorListModel });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/directors/create")]
        public async Task<IActionResult> Create(DirectorCombinedVM vm)
        {
            PaginationVM pagination = this.GetCurrentPagination();

            if (!this.ModelState.IsValid)
            {
                this.AddAlert(false, "An error has occured");
                return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, name = this.Request.Query["name"] });
            }

            var director = Mapper.Map<Director>(vm.Director);

            await this.directorService.Create(director);

            this.AddAlert(true, "Successfully added director");

            return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, name = this.Request.Query["name"] });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/directors/delete")]
        public async Task<IActionResult> Delete(string directorId)
        {
            if (string.IsNullOrWhiteSpace(directorId))
            {
                return this.BadRequest($"invalid director id");
            }

            PaginationVM pagination = this.GetCurrentPagination();

            await this.directorService.Delete(directorId);

            this.AddAlert(true, "Successfully deleted director");

            return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, name = this.Request.Query["name"] });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/directors/restore")]
        public async Task<IActionResult> Restore(string directorId)
        {
            if (string.IsNullOrWhiteSpace(directorId))
            {
                return this.BadRequest($"invalid director id");
            }

            PaginationVM pagination = this.GetCurrentPagination();

            await this.directorService.Restore(directorId);

            this.AddAlert(true, "Successfully restored director");

            return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, name = this.Request.Query["name"] });
        }
    }
}