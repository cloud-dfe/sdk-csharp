using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sdk.CloudDfe
{
    public class Gnre : Base
    {
        public Gnre(Dictionary<string, object> config) : base(config){}

        // Rotas Dfe

        public async Task<Dictionary<string, object>> Consulta(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/gnre/{key}");
            return resp;
        }
        public async Task<Dictionary<string, object>> Cria(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/gnre", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Mostra(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("POST", "/gnre/configuf", payload);
            return resp;
        }
    }
}
