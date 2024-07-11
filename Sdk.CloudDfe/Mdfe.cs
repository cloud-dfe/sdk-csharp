namespace Sdk.CloudDfe
{
    public class Mdfe(Dictionary<string, object> config) : Base(config)
    {

        // Rotas Dfe

        public async Task<Dictionary<string, object>> Cria(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/mdfe", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Preview(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/mdfe/preview", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Status()
        {
            var resp = await _client.Send("GET", "/mdfe/status");
            return resp;
        }
        public async Task<Dictionary<string, object>> Consulta(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/mdfe/{key}");
            return resp;
        }
        public async Task<Dictionary<string, object>> Busca(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/mdfe/busca", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Cancela(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/mdfe/cancela", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Encerra(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/mdfe/encerra", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Condutor(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/mdfe/condutor", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Offline()
        {
            var resp = await _client.Send("GET", "/mdfe/offline");
            return resp;
        }
        public async Task<Dictionary<string, object>> Pdf(Dictionary<string, object> payload)
        {
            var key = CheckKey(payload);
            var resp = await _client.Send("GET", $"/mdfe/pdf/{key}", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Backup(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/mdfe/backup", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Nfe(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/mdfe/nfe", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Abertos()
        {
            var resp = await _client.Send("GET", "/mdfe/abertos");
            return resp;
        }
        public async Task<Dictionary<string, object>> Importa(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/mdfe/importa", payload);
            return resp;
        }
    }
}
