using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sdk.CloudDfe
{
    public class Emitente : Base
    {
        public Emitente(Dictionary<string, object> config) : base(config)
        {
        }
        // Rotas Dfe

        public async Task<Dictionary<string, object>> Token()
        {
            var resp = await _client.Send("GET", "/emitente/token");
            return resp;
        }
        public async Task<Dictionary<string, object>> Atualiza(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("PUT", "/emitente", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Mostra()
        {
            var resp = await _client.Send("GET", "/emitente");
            return resp;
        }
    }
}
