using System.Collections.Generic;
using System.Threading.Tasks;

namespace sdk_cloud_dfe
{
    public class Dfe : Base
    {
        public Dfe(Dictionary<string, object> config) : base(config){}

        // Rotas Dfe

        public async Task<Dictionary<string, object>> BuscaCte(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/dfe/cte", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> DownloadCte(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/dfe/cte/{key}", null);
            return resp;
        }
        public async Task<Dictionary<string, object>> BuscaNfe(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/dfe/nfe", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> DownloadNfe(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/dfe/nfe/{key}", null);
            return resp;
        }
        public async Task<Dictionary<string, object>> BuscaNfse(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/dfe/nfse", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> DownloadNfse(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/dfe/nfse/{key}", null);
            return resp;
        }
        public async Task<Dictionary<string, object>> Eventos(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/dfe/eventos/{key}", null);
            return resp;
        }
        public async Task<Dictionary<string, object>> Backup(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/dfe/backup", payload);
            return resp;
        }
    }
}
