using System.Collections.Generic;
using System.Threading.Tasks;

namespace sdk_cloud_dfe
{
    public class Nfce : Base
    {
        public Nfce(Dictionary<string, object> config) : base(config){}

        // Rotas Nfce

        public async Task<Dictionary<string, object>> Cria(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfce", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Preview(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfce/preview", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Status()
        {
            var resp = await _client.Send("GET", "/nfce/status", null);
            return resp;
        }
        public async Task<Dictionary<string, object>> Consulta(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/nfce/{key}", null);
            return resp;
        }
        public async Task<Dictionary<string, object>> Busca(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfce/busca", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Cancela(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("GET", "/nfce/cancela", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Offline(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("GET", "/nfce/offline", null);
            return resp;
        }
        public async Task<Dictionary<string, object>> Inutiliza(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("GET", "/nfce/inutiliza", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Pdf(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/nfce/pdf/{key}", null);
            return resp;
        }
        public async Task<Dictionary<string, object>> Substitui(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfce/substitui", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Backup(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfce/backup", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Importa(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfce/importa", payload);
            return resp;
        }
    }
}
