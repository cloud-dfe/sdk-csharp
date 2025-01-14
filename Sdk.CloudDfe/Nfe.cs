using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sdk.CloudDfe
{
    public class Nfe : Base
    {
        public Nfe(Dictionary<string, object> config) : base(config)
        {
        }
        public async Task<Dictionary<string, object>> Cria(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfe", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Preview(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfe/preview", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Status()
        {
            var resp = await _client.Send("GET", "/nfe/status");
            return resp;
        }
        public async Task<Dictionary<string, object>> Consulta(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/nfe/{key}");
            return resp;
        }
        public async Task<Dictionary<string, object>> Busca(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfe/busca", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Cancela(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfe/cancela", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Correcao(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfe/correcao", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Inutiliza(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfe/inutiliza", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Pdf(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/nfe/pdf/{key}");
            return resp;
        }
        public async Task<Dictionary<string, object>> Etiqueta(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/nfe/pdf/etiqueta/{key}");
            return resp;
        }
        public async Task<Dictionary<string, object>> Manifesta(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfe/manifesta", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Backup(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfe/backup", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Download(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/nfe/download/{key}");
            return resp;
        }
        public async Task<Dictionary<string, object>> Recebidas(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfe/recebidas", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Interessado(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfe/interessado", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Importa(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfe/importa", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Comprovante(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfe/comprovante", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Cadastro(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/nfe/cadastro", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Simples(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/nfe/pdf/simples/{key}");
            return resp;
        }
    }
}
