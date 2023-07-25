using EventManagementSystem.Commons;
using EventManagementSystem.Ticketmaster.Exceptions;
using EventManagementSystem.Ticketmaster.Models.Response;
using EventManagementSystem.Ticketmaster.Options;
using Flurl;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;

namespace EventManagementSystem.Ticketmaster.Util
{
    public class TicketmasterClient
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<GatewayOptions> _options;
        public TicketmasterClient(IOptions<GatewayOptions> options)
        {
            _options = options;

            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(
                    $"{options.Value.Url}/{options.Value.Package}/v{options.Value.Version}/"
                )
            };
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");
        }

        /*public async Task<TResponse> GetAsync<TResponse>(HttpRequest request, CancellationToken cancellationToken = default) where TResponse : class
        {
            var response = await _httpClient.GetAsync("");
            return response.Content;
        }*/
        public async Task<TResponse> GetAsync<TResponse>(Uri uri) where TResponse : class
        {
            uri = new Url(uri).SetQueryParam("apikeys", _options.Value.ApiKey).ToUri();

            var response = await _httpClient.GetAsync(uri);

            return await HandleResponse<TResponse>(response);
        }

        public async Task<TResponse> HandleResponse<TResponse>(HttpResponseMessage response)
            where TResponse : class
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var responseT = JsonConvert.DeserializeObject<TResponse>(jsonResponse);
                return responseT;
            }

            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    var errorResponse =
                        JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse).Error;
                    throw new TicketmasterBadRequestException(
                        ErrorCode.BAD_REQUEST,
                        errorResponse.Message,
                        errorResponse.Code.ToString()
                    );
                case HttpStatusCode.NotFound:

            }
            return null;
        }
    }
}
