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
    using MovieDatabase.Web.Areas.Admin.Models.Actors;

    public class ActorsController : EntityListController
    {
        private ICrudService<Actor> actorService;

        public ActorsController(ICrudService<Actor> actorService)
        {
            this.actorService = actorService;
        }

        [HttpGet]
        [Route("admin/actors")]
        public IActionResult Index(PaginationVM pagination, string name)
        {
            if (this.HasAlert)
            {
                this.SetAlertModel();
            }

            var actorsQuery = this.actorService.GetAllWithDeleted();

            if (!string.IsNullOrWhiteSpace(name))
            {
                actorsQuery = actorsQuery.Where(a => a.Name.ToLower().Contains(name.ToLower()));
            }

            actorsQuery = actorsQuery.OrderBy(u => u.IsDeleted).ThenByDescending(u => u.Name);

            var paginatedActors = this.PaginateList<ActorVM>(pagination, actorsQuery.ProjectTo<ActorVM>()).ToList();

            int totalPages = this.GetTotalPages(pagination.PageSize, actorsQuery.Count());

            ActorListVM actorListModel = new ActorListVM
            {
                Actors = paginatedActors,
                NextPage = pagination.Page < totalPages ? pagination.Page + 1 : pagination.Page,
                PreviousPage = pagination.Page > 1 ? pagination.Page - 1 : pagination.Page,
                CurrentPage = pagination.Page,
                TotalPages = totalPages,
                ShowPagination = totalPages > 1,
            };

            return this.View(new ActorCombinedVM { ActorList = actorListModel });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/actors/create")]
        public async Task<IActionResult> Create(ActorCombinedVM vm)
        {
            PaginationVM pagination = this.GetCurrentPagination();

            if (!this.ModelState.IsValid)
            {
                this.AddAlert(false, "An error has occured");
                return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, name = this.Request.Query["name"] });
            }

            var actor = Mapper.Map<Actor>(vm.Actor);

            await this.actorService.Create(actor);

            this.AddAlert(true, "Successfully added actor");

            return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, name = this.Request.Query["name"] });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/actors/delete")]
        public async Task<IActionResult> Delete(string actorId)
        {
            if (string.IsNullOrWhiteSpace(actorId))
            {
                return this.BadRequest($"invalid actor id");
            }

            PaginationVM pagination = this.GetCurrentPagination();

            await this.actorService.Delete(actorId);

            this.AddAlert(true, "Successfully deleted actor");

            return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, name = this.Request.Query["name"] });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/actors/restore")]
        public async Task<IActionResult> Restore(string actorId)
        {
            if (string.IsNullOrWhiteSpace(actorId))
            {
                return this.BadRequest($"invalid actor id");
            }

            PaginationVM pagination = this.GetCurrentPagination();

            await this.actorService.Restore(actorId);

            this.AddAlert(true, "Successfully restored actor");

            return this.RedirectToAction("Index", new { pagination.Page, pagination.PageSize, name = this.Request.Query["name"] });
        }

        //public async Task<IActionResult> Edit(string id)
        //{
        //    var actor = await this.actorService.Get(id);
        //    var actorVm = Mapper.Map<ActorVM>(actor);

        //    return this.View(actorVm);
        //}

        //public async Task<IActionResult> Edit(ActorVM vm)
        //{
        //    await this.actorService.Update(Mapper.Map<Actor>(vm));

        //    return this.RedirectToAction("Index");
        //}
    }
}