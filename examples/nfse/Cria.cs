using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOiJ0b2tlbl9leGVtcGxvIiwidXNyIjoidGsiLCJ0cCI6InRrIn0.Tva_viCMCeG3nkRYmi_RcJ6BtSzui60kdzIsuq5X-sQ" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var nfse = new Nfse(config);

var payload = new Dictionary<string, object>
{
    { "numero", "1" },
    { "serie", "0" },
    { "tipo", "1" },
    { "status", "1" },
    { "data_emissao", "2017-12-27T17:43:14-03:00" },
    { "tomador", new Dictionary<string, object>
        {
            { "cnpj", "12345678901234" },
            { "cpf", null },
            { "im", null },
            { "razao_social", "Fake Tecnologia Ltda" },
            { "endereco", new Dictionary<string, object>
                {
                    { "logradouro", "Rua New Horizon" },
                    { "numero", "16" },
                    { "complemento", null },
                    { "bairro", "Jardim America" },
                    { "codigo_municipio", "4119905" },
                    { "uf", "PR" },
                    { "cep", "81530945" }
                }
            }
        }
    },
    { "servico", new Dictionary<string, object>
        {
            { "codigo_municipio", "4119905" },
            { "itens", new List<Dictionary<string, object>> {
                new Dictionary<string, object> {
                    { "codigo_tributacao_municipio", "10500" },
                    { "discriminacao", "Exemplo Serviço" },
                    { "valor_servicos", "1.00" },
                    { "valor_pis", "1.00" },
                    { "valor_cofins", "1.00" },
                    { "valor_inss", "1.00" },
                    { "valor_ir", "1.00" },
                    { "valor_csll", "1.00" },
                    { "valor_outras", "1.00" },
                    { "valor_aliquota", "1.00" },
                    { "valor_desconto_incondicionado", "1.00" }
                }
            }}
        }
    },
    { "intermediario", new Dictionary<string, object>
        {
            { "cnpj", "12345678901234" },
            { "cpf", null },
            { "im", null },
            { "razao_social", "Fake Tecnologia Ltda" }
        }
    },
    { "obra", new Dictionary<string, object>
        {
            { "codigo", "2222" },
            { "art", "1111" }
        }
    }
};

try{
    var resp = Task.Run(async () => await nfse.Cria(payload)).GetAwaiter().GetResult();

    if (resp.ContainsKey("sucesso") && (bool)resp["sucesso"]){
        var chave = resp["chave"].ToString();
        var payloadConsulta = new Dictionary<string, object> { { "chave", chave } };
        
        Thread.Sleep(15000);

        var respC = Task.Run(async () => await nfse.Consulta(payloadConsulta)).GetAwaiter().GetResult();
        
        if (!respC.ContainsKey("codigo") || Convert.ToInt32(respC["codigo"]) != 5023){
            if (respC.ContainsKey("sucesso") && (bool)respC["sucesso"]){
                // autorizado
                string jsonOutput = JsonConvert.SerializeObject(respC, Formatting.Indented);
                Console.WriteLine(jsonOutput);
            } else {
                // rejeitação
                string jsonOutput = JsonConvert.SerializeObject(respC, Formatting.Indented);
                Console.WriteLine(jsonOutput);
            }
        } else {
            // nota em processamento
            // recomendamos que seja utilizado o metodo de consulta manual ou o webhook
            string jsonOutput = JsonConvert.SerializeObject(respC, Formatting.Indented);
            Console.WriteLine(jsonOutput);
        }
    }
    else if (resp.ContainsKey("codigo") && (Convert.ToInt32(resp["codigo"]) == 5001 || Convert.ToInt32(resp["codigo"]) == 5002))
    {
        if (resp.ContainsKey("erros"))
        {
            var erros = resp["erros"].ToString();
            Console.WriteLine(erros);
        }
    }
    else if (resp.ContainsKey("codigo") && (Convert.ToInt32(resp["codigo"]) == 5008))
    {
        var chave = resp["chave"].ToString();
        string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
        Console.WriteLine(jsonOutput);

        var payloadConsulta = new Dictionary<string, object> { { "chave", chave } };
        var respC = Task.Run(async () => await nfse.Consulta(payloadConsulta)).GetAwaiter().GetResult();

        if (!respC.ContainsKey("codigo") || Convert.ToInt32(respC["codigo"]) != 5023) {
            if (respC.ContainsKey("sucesso") && (bool)respC["sucesso"])
            {
                string jsonOutput = JsonConvert.SerializeObject(respC, Formatting.Indented);
                Console.WriteLine(jsonOutput);
            } else {
                string jsonOutput = JsonConvert.SerializeObject(respC, Formatting.Indented);
                Console.WriteLine(jsonOutput);
            }
        } else {
            string jsonOutput = JsonConvert.SerializeObject(respC, Formatting.Indented);
            Console.WriteLine(jsonOutput);
        }
    }
    else
    {
        string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
        Console.WriteLine(jsonOutput);
    }

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
