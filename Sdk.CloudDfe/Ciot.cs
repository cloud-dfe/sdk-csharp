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
            var resp = await _client.Send("POST", "/ciot/ailog", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Consulta(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/ciot/ailog/{key}");
            return resp;
        }
        public async Task<Dictionary<string, object>> Busca(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/ciot/ailog/busca", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Cancela(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/ciot/ailog/cancela", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Encerra(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/ciot/ailog/encerra", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Retifica(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/ciot/ailog/retifica", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Pdf(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/ciot/ailog/pdf/{key}");
            return resp;
        }
        public async Task<Dictionary<string, object>> Transportador(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/ciot/ailog/transportador", payload);
            return resp;
        }
    }
}
