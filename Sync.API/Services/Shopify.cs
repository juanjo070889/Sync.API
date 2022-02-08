using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Sync.API.Persistance.DTO;
using System.Net;

namespace Sync.API.Services
{
    public class Shopify : IShopify
    {
        private readonly IOptions<AppSettings> _settings;
        private HttpClient _httpClient;

        private readonly string _remoteServiceBaseUrl;
        private readonly IStringLocalizer<Resources.SharedMessages> _sharedMessagesLocalizer;

        public Shopify(HttpClient httpClient,
                               IOptions<AppSettings> settings,
                               IStringLocalizer<Resources.SharedMessages> sharedMessagesLocalizer)
        {
            _httpClient = httpClient;
            _settings = settings;
            _sharedMessagesLocalizer = sharedMessagesLocalizer;
            _remoteServiceBaseUrl = $"{_settings.Value.ShopifyAPIEndpoint}";
        }

        public static string GetServiceResponseErrorMessage(string responseContent)
        {
            string message = null;
            if (!string.IsNullOrEmpty(responseContent))
            {
                var responseObjectDefinition = new { Message = "" };
                var serviceMessage = JsonConvert.DeserializeAnonymousType(responseContent, responseObjectDefinition);
                message = serviceMessage.Message;
            }
            return message;
        }

        public async Task<ProductsDTO> GetAllProducts()
        {
            ProductsDTO response;
            var uri = $"{_remoteServiceBaseUrl}products.json";

            var clientHandler = new HttpClientHandler
            {
                Credentials = new NetworkCredential(_settings.Value.APIkey, _settings.Value.APIpass),
                PreAuthenticate = true
            };

            _httpClient = new HttpClient(clientHandler);
            _httpClient.BaseAddress = new Uri(uri);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("X-Shopify-Access-Token", _settings.Value.APIpass);

            var httpResponse = await _httpClient.GetAsync(uri);
            var responseString = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                string errorMessage = GetServiceResponseErrorMessage(responseContent);
                throw new Exception(errorMessage);
            }

            response = JsonConvert.DeserializeObject<ProductsDTO>(responseString);
            return response;
        }

        public async Task<VariantsDTO> GetProductVariant(string productId, string sku)
        {
            VariantsDTO response;
            var uri = $"{_remoteServiceBaseUrl}products/{productId}/variants.json?sku={sku}";

            var clientHandler = new HttpClientHandler
            {
                Credentials = new NetworkCredential(_settings.Value.APIkey, _settings.Value.APIpass),
                PreAuthenticate = true
            };

            _httpClient = new HttpClient(clientHandler);
            _httpClient.BaseAddress = new Uri(uri);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("X-Shopify-Access-Token", _settings.Value.APIpass);

            var httpResponse = await _httpClient.GetAsync(uri);
            var responseString = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                string errorMessage = GetServiceResponseErrorMessage(responseContent);
                throw new Exception(errorMessage);
            }

            response = JsonConvert.DeserializeObject<VariantsDTO>(responseString);
            return response;
        }

        public async Task UpdateProductVariant(Variant variant, int quantity)
        {
            var uri = $"{_remoteServiceBaseUrl}";

            var clientHandler = new HttpClientHandler
            {
                Credentials = new NetworkCredential(_settings.Value.APIkey, _settings.Value.APIpass),
                PreAuthenticate = true
            };

            _httpClient = new HttpClient(clientHandler);
            _httpClient.BaseAddress = new Uri(uri);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("X-Shopify-Access-Token", _settings.Value.APIpass);

         
            var shopify = new
            {

                    inventory_item_id = variant.inventory_item_id,
                    available = quantity,
                    location_id = "67300950240"
                    
            };

            var method = $"inventory_levels/set.json";
            var response = await _httpClient.PostAsJsonAsync(method, shopify) ;

            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                string errorMessage = GetServiceResponseErrorMessage(responseContent);
                throw new Exception(errorMessage);
            }
        }

        public async Task<OrderDTO> GetAllOrdersPaid()
        {
            OrderDTO response;
            var uri = $"{_remoteServiceBaseUrl}orders.json?financial_status=paid";

            var clientHandler = new HttpClientHandler
            {
                Credentials = new NetworkCredential(_settings.Value.APIkey, _settings.Value.APIpass),
                PreAuthenticate = true
            };

            _httpClient = new HttpClient(clientHandler);
            _httpClient.BaseAddress = new Uri(uri);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("X-Shopify-Access-Token", _settings.Value.APIpass);

            var httpResponse = await _httpClient.GetAsync(uri);
            var responseString = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                string errorMessage = GetServiceResponseErrorMessage(responseContent);
                throw new Exception(errorMessage);
            }

            response = JsonConvert.DeserializeObject<OrderDTO>(responseString);
            return response;
        }

    }
}

