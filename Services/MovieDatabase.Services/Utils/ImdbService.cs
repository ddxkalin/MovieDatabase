namespace MovieDatabase.Services.Utils
{
    using System.Collections.Generic;

    using Microsoft.Extensions.Options;
    using MovieDatabase.Common.Settings;
    using MovieDatabase.Services.Models;
    using RestSharp;

    public class ImdbService
    {
        private string apiAddress;
        private string apiKey;

        public ImdbService(IOptions<ImdbServiceSettings> settings)
        {
            this.apiAddress = settings.Value.ApiAddress;
            this.apiKey = settings.Value.ApiKey;
        }

        public ImdbServiceMovieModel GetMovieByTitle(string title)
        {
            var client = new RestClient(this.apiAddress);

            var request = this.CreateRequest("/", Method.GET, new Dictionary<string, string> { { "apiKey",  this.apiKey }, { "t", title } });

            RestResponse<ImdbServiceMovieModel> response = (RestResponse<ImdbServiceMovieModel>)client.Execute<ImdbServiceMovieModel>(request);

            return response.Data;
        }

        private RestRequest CreateRequest(string endpoint, Method method, Dictionary<string, string> urlParams)
        {
            var request = new RestRequest(endpoint, method);

            foreach (KeyValuePair<string, string> entry in urlParams)
            {
                request.AddParameter(entry.Key, entry.Value);
            }

            return request;
        }
    }
}
