namespace Sdk.CloudDfe
{
    public class Cte(Dictionary<string, object> config) : Base(config)
    {

        // Rotas Cte

        public async Task<Dictionary<string, object>> Status()
        {
            var resp = await _client.Send("GET", "/cte/status");
            return resp;
        }
        public async Task<Dictionary<string, object>> Consulta(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/cte/{key}");
            return resp;
        }
        public async Task<Dictionary<string, object>> Pdf(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/cte/pdf/{key}");
            return resp;
        }
        public async Task<Dictionary<string, object>> Cria(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/cte", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Busca(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/cte/busca", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Cancela(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/cte/cancela", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Correcao(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/cte/correcao", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Inutiliza(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/cte/inutiliza", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Backup(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/cte/backup", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Importa(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/cte/importa", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Preview(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/cte/preview", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Desacordo(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/cte/desacordo", payload);
            return resp;
        }
    }
}
