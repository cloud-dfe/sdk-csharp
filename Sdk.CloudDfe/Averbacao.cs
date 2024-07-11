namespace Sdk.CloudDfe
{
    public class Averbacao(Dictionary<string, object> config) : Base(config)
    {

        // Rotas Averbação

        public async Task<Dictionary<string, object>> Atm(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/averbacao/atm", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> AtmCancela(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/averbacao/atm/cancela", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Elt(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/averbacao/elt", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> PortoSeguro(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/averbacao/portoseguro", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> PortoSeguroCancela(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/averbacao/portoseguro/cancela", payload);
            return resp;
        }
    }
}
