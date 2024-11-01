#pragma warning disable CS8601, CS8618, CS8600, CS8603 // Caso for alterar toda estrutura do SDK habilite os erros

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Sdk.CloudDfe
{
    public class Service
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseURI;
        private readonly string _token;
        private readonly int _timeout;
        private readonly bool _debug;

        public Service(Dictionary<string, object> config)
        {
            _baseURI = config["baseURI"].ToString();
            _token = config["token"].ToString();
            _timeout = config.ContainsKey("timeout") ? Convert.ToInt32(config["timeout"]) : 60;
            _debug = config.ContainsKey("debug") ? Convert.ToBoolean(config["debug"]) : false;

            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(_timeout)
            };
        }

        public async Task<Dictionary<string, object>> Request(string method, string route, Dictionary<string, object> payload = null)
        {
            var headers = new Dictionary<string, string>
            {
                { "Authorization", _token },
                { "Accept", "application/json" }
            };

            var jsonData = payload != null ? JsonConvert.SerializeObject(payload) : "{}";
            var url = new Uri($"{_baseURI}{route}");

            if (_debug)
            {
                Console.WriteLine(url);
            }

            var request = new HttpRequestMessage(new HttpMethod(method), url)
            {
                Content = payload != null ? new StringContent(jsonData, Encoding.UTF8, "application/json") : null
            };

            foreach (var header in headers)
            {
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            try
            {
                var response = await _httpClient.SendAsync(request);
                var responseJson = JObject.Parse(await response.Content.ReadAsStringAsync());
                return responseJson.ToObject<Dictionary<string, object>>();
            }
            catch (Exception ex)
            {
                if (_debug)
                {
                    Console.WriteLine($"Erro ao enviar a requisição: {ex.Message}");
                }
                throw new ArgumentException("Não foi possível fazer a comunicação.");
            }
        }
    }

}