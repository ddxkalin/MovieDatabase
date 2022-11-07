namespace MovieDatabase.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Web.Controllers.Base;
    using MovieDatabase.Web.ViewModels;
    using Newtonsoft.Json;
    using RestSharp;
    using RestSharp.Serialization.Json;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            var client = new RestClient("https://api.predicthq.com/v1/events/");
            client.AddDefaultHeader("Authorization", "Bearer ZN79Mw9lMMWGgNMDSA3NwO5MpoczZI");
            client.AddDefaultHeader("Accept", "application/json");
            var restRequest = new RestRequest();
            restRequest.AddQueryParameter("category", "festivals");
            restRequest.AddQueryParameter("label", "movie");
            restRequest.AddQueryParameter("q", "film");
            restRequest.AddQueryParameter("offset", "4");
            var response = client.Get(restRequest);
            var movieFestivalsList = new List<MovieFestival>();

            try
            {
                if (response.IsSuccessful)
                {
                    var results = JsonConvert.DeserializeObject<MovieFestivalsResult>(response.Content);
                    movieFestivalsList = results.Results;
                }
            }
            catch
            {
            }

            return this.View(movieFestivalsList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View();
    }
}