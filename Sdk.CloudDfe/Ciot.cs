using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sdk.CloudDfe
{
    public class Ciot : Base
    {
        public Ciot(Dictionary<string, object> config) : base(config)
        {
        }
        // Rotas Ciot

        public async Task<Dictionary<string, object>> Cria(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/ciot", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Consulta(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/ciot/{key}");
            return resp;
        }
        public async Task<Dictionary<string, object>> Busca(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/ciot/busca", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Cancela(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/ciot/cancela", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Encerra(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/ciot/encerra", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Retifica(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/ciot/retifica", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Pdf(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/ciot/pdf/{key}");
            return resp;
        }
        public async Task<Dictionary<string, object>> Transportador(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/ciot/transportador", payload);
            return resp;
        }
    }
}
