namespace MovieDatabase.Web.ViewModels
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class MovieFestivalsResult
    {
        [JsonProperty("results")]
        public List<MovieFestival> Results { get; set; }
    }
}