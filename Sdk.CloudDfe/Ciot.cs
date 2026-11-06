using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sdk.CloudDfe
{
    public class Ciot : Base
    {
        public Ciot(Dictionary<string, object> config) : base(config)
        {
        }
        // Rotas Ciot

        public async Task<Dictionary<string, object>> Cria(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/ciot", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Consulta(Dictionary<string, object> payload)
        {
            var identificacao = CheckIdentificacao(payload);
            var resp = await _client.Send("GET", $"/ciot/{identificacao}");
            return resp;
        }
        public async Task<Dictionary<string, object>> Busca(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/ciot/busca", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Cancela(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/ciot/cancela", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Encerra(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/ciot/encerra", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Retifica(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/ciot/retifica", payload);
            return resp;
        }
        public async Task<Dictionary<string, object>> Pdf(Dictionary<string, object> payload)
        {
            var identificacao = CheckIdentificacao(payload);
            var resp = await _client.Send("GET", $"/ciot/pdf/{identificacao}");
            return resp;
        }
        public async Task<Dictionary<string, object>> Transportador(Dictionary<string, object> payload)
        {
            var resp = await _client.Send("POST", "/ciot/transportador", payload);
            return resp;
        }

        // o CIOT é identificado pela identificação informada na criação, não por chave de acesso
        public string CheckIdentificacao(Dictionary<string, object> payload)
        {
            var identificacao = payload.ContainsKey("identificacao") && payload["identificacao"] != null
                ? payload["identificacao"].ToString().Trim()
                : "";
            if (!Regex.IsMatch(identificacao, @"^[A-Za-z0-9._-]{1,40}$"))
            {
                throw new ArgumentException("A identificação do CIOT deve ter de 1 a 40 caracteres: letras, números, ponto, hífen ou sublinhado.");
            }
            return Uri.EscapeDataString(identificacao);
        }
    }
}
