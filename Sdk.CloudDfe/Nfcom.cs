using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sdk.CloudDfe
{
    public class Nfcom : Base
    {
        public Nfcom(Dictionary<string, object> config) : base(config)
        {
        }
        // Rotas Nfcom

        public async Task<Dictionary<string, object>> Status()
        {
            var resp = await _client.Send("GET", "/nfcom/status");
            return resp;
        }
        public async Task<Dictionary<string, object>> Cria(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfcom", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Consulta(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/nfcom/{key}");
            return resp;
        }
        public async Task<Dictionary<string, object>> Cancela(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfcom/cancela", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Busca(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfcom/busca", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Pdf(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/nfcom/pdf/{key}");
            return resp;
        }
        public async Task<Dictionary<string, object>> Preview(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfcom/preview", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Backup(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfcom/backup", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Importa(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfcom/importa", payload);
            return resp;
        }
    }
}