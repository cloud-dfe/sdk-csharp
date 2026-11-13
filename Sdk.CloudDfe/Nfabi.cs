using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sdk.CloudDfe
{
    public class Nfabi : Base
    {
        public Nfabi(Dictionary<string, object> config) : base(config)
        {
        }
        // Rotas Nfabi

        public async Task<Dictionary<string, object>> Status()
        {
            var resp = await _client.Send("GET", "/nfabi/status");
            return resp;
        }
        public async Task<Dictionary<string, object>> Cria(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfabi", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Consulta(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/nfabi/{key}");
            return resp;
        }
        public async Task<Dictionary<string, object>> Cancela(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfabi/cancela", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Parcela(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfabi/parcela", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Desfazimento(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfabi/desfazimento", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Busca(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfabi/busca", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Pdf(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/nfabi/pdf/{key}");
            return resp;
        }
        public async Task<Dictionary<string, object>> Preview(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfabi/preview", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Backup(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfabi/backup", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Importa(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfabi/importa", payload);
            return resp;
        }
    }
}