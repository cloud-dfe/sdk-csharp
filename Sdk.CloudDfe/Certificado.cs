using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sdk.CloudDfe
{
    public class Certificado : Base
    {
        public Certificado(Dictionary<string, object> config) : base(config)
        {
        }
        // Rotas Certificado
        public async Task<Dictionary<string, object>> Atualiza(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/certificado", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Mostra()
        {
            var resp = await _client.Send("GET", "/certificado");
            return resp;
        }

    }
}
