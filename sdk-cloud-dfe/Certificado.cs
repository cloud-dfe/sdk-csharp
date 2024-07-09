using System.Collections.Generic;
using System.Threading.Tasks;

namespace sdk_cloud_dfe
{
    public class Certificado : Base
    {
        public Certificado(Dictionary<string, object> config) : base(config){}

        // Rotas Certificado

        public async Task<Dictionary<string, object>> Atualiza(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/certificado", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Mostra()
        {
            var resp = await _client.Send("GET", "/certificado", null);
            return resp;
        }

    }
}
