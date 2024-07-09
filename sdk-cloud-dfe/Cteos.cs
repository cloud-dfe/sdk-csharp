using System.Collections.Generic;
using System.Threading.Tasks;

namespace sdk_cloud_dfe
{
    public class Cteos : Base
    {
        public Cteos(Dictionary<string, object> config) : base(config){}

        // Rotas CteOS

        public async Task<Dictionary<string, object>> Status()
        {
            var resp = await _client.Send("GET", "/cteos/status", null);
            return resp;
        }
        public async Task<Dictionary<string, object>> Consulta(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/cteos{key}", null);
            return resp;
        }
        public async Task<Dictionary<string, object>> Pdf(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("POST", $"/cteos/pdf/{key}", null);
            return resp;
        }
        public async Task<Dictionary<string, object>> Cria(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/cteos", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Busca(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/cteos/busca", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Cancela(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/cteos/cancela", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Correcao(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/cteos/correcao", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Inutiliza(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/cteos/inutiliza", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Backup(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/cteos/backup", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Importa(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/cteos/importa", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Preview(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/cteos/preview", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Desacordo(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/cteos/desacordo", payload);
            return resp;
        }
    }
}
