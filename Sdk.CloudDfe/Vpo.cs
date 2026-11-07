using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sdk.CloudDfe
{
    public class Vpo : Base
    {
        public Vpo(Dictionary<string, object> config) : base(config)
        {
        }
        // Rotas Vpo

        public async Task<Dictionary<string, object>> Cria(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/vpo/ailog", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Consulta(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/vpo/ailog/{key}");
            return resp;
        }
        public async Task<Dictionary<string, object>> Busca(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/vpo/ailog/busca", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Cancela(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/vpo/ailog/cancela", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Reemite(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/vpo/ailog/reemite", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Rota(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/vpo/ailog/rota", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Pdf(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/vpo/ailog/pdf/{key}");
            return resp;
        }
        public async Task<Dictionary<string, object>> Veiculo(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/vpo/ailog/veiculo", payload);
            return resp;
        }
    }
}
