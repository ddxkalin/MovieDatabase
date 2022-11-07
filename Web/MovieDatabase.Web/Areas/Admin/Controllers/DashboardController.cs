namespace MovieDatabase.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Common;
    using MovieDatabase.Web.Areas.Admin.Controllers.Base;

    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}