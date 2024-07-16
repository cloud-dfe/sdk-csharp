#pragma warning disable CS8600 // Caso for alterar toda estrutura do SDK habilite os erros

namespace Sdk.CloudDfe
{
    public class Softhouse(Dictionary<string, object> config) : Base(config)
    {
        public async Task<Dictionary<string, object>> CriaEmitente(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/soft/emitente", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> AtualizaEmitente(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("PUT", "/soft/emitente", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> MostraEmitente(Dictionary<string, object> payload)
        {
            if (!payload.ContainsKey("doc")) {
                throw new Exception("O doc não foi informado no processo.");
            }

            var doc = payload["doc"].ToString();
            var resp = await _client.Send("GET", $"/soft/emitente/{doc}");
            return resp;
        }
        public async Task<Dictionary<string, object>> ListaEmitente(Dictionary<string, object> payload)
        {
            string status = payload.ContainsKey("status") ? payload["status"].ToString() : "";
            string rota = "/soft/emitente";
    
            if (status == "deletados" || status == "inativos")
            {
                rota = "/soft/emitente/deletados";
            }
            
            var resp = await _client.Send("GET", rota);
            return resp;
        }
        public async Task<Dictionary<string, object>> DeletaEmitente(Dictionary<string, object> payload)
        {
            if (payload == null || !payload.ContainsKey("doc") || string.IsNullOrEmpty(payload["doc"].ToString()))
            {
                throw new Exception("Deve ser passado um CNPJ ou um CPF para efetuar a deleçao do emitente.");
            }

            string doc = payload["doc"].ToString();

            var resp = await _client.Send("DELETE", $"/soft/emitente/{doc}");
            return resp;
        }
    }
}