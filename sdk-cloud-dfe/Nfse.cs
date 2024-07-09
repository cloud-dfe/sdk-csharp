using System.Collections.Generic;
using System.Threading.Tasks;

namespace sdk_cloud_dfe
{
    public class Nfse : Base
    {
        public Nfse(Dictionary<string, object> config) : base(config){}

        public async Task<Dictionary<string, object>> Cria(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfse", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Preview(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfse/preview", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Pdf(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET",  $"/nfse/pdf/{key}", null);
            return resp;
        }
        public async Task<Dictionary<string, object>> Consulta(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET",  $"/nfse/{key}", null);
            return resp;
        }
        public async Task<Dictionary<string, object>> Cancela(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfse/cancela", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Substitui(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfse/substitui", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Busca(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfse/busca", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Backup(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfse/backup", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Localiza(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfse/consulta", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Info(Dictionary<string, object> payload)
        {
            var ibge = payload["ibge"].ToString()
            var resp = await _client.Send("GET", $"/nfse/info/{ibge}", null);
            return resp;
        }
        public async Task<Dictionary<string, object>> Conflito(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfse/conflito", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Offline()
        {
            var resp = await _client.Send("GET", "/nfse/offline", null);
            return resp;
        }
        public async Task<Dictionary<string, object>> Resolve(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/nfse/resolve/{key}", null);
            return resp;
        }
    }
}
