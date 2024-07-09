using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace sdk_cloud_dfe
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

        public async Task<Dictionary<string, object>> Request(string method, string route, Dictionary<string, object> payload)
        {
            var headers = new Dictionary<string, string>
            {
                { "Authorization", _token },
                { "Accept", "application/json" },
                { "Content-Type", "application/json" }
            };

            var jsonData = JsonConvert.SerializeObject(payload);
            var url = new Uri($"{_baseURI}{route}");

            var request = new HttpRequestMessage(new HttpMethod(method), url)
            {
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
            };

            foreach (var header in headers)
            {
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            try
            {
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseJson = JObject.Parse(await response.Content.ReadAsStringAsync());

                Dictionary<string, object> result = responseJson.ToObject<Dictionary<string, object>>();
                
                return result;
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