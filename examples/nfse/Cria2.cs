using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOiJ0b2tlbl9leGVtcGxvIiwidXNyIjoidGsiLCJ0cCI6InRrIn0.Tva_viCMCeG3nkRYmi_RcJ6BtSzui60kdzIsuq5X-sQ" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "version", "2" },
    { "timeout", 60 },
    { "debug", true }
};

var nfse = new Nfse(config);

var payload = new Dictionary<string, object>
{
    { "numero", "" },
    { "serie", "" },
    { "tipo", "" },
    { "status", "" },
    { "data_emissao", "" },

    { "tomador", new Dictionary<string, object>
        {
            { "cnpj", "" },
            { "cpf", "" },
            { "im", "" },
            { "razao_social", "" },

            { "endereco", new Dictionary<string, object>
                {
                    { "logradouro", "" },
                    { "numero", "" },
                    { "complemento", "" },
                    { "bairro", "" },
                    { "codigo_municipio", "" },
                    { "uf", "" },
                    { "cep", "" }
                }
            }
        }
    },

    { "servico", new Dictionary<string, object>
        {
            { "endereco_local_prestacao", new Dictionary<string, object>
                {
                    { "codigo_municipio", "" },
                    { "codigo_municipio_prestacao", "" },
                    { "codigo_pais", "" }
                }
            },

            { "codigo", "" },
            { "codigo_tributacao_municipio", "" },
            { "discriminacao", "" },
            { "valor_servicos", "" },
            { "valor_desconto_incondicionado", "" },

            { "tributos_municipais", new Dictionary<string, object>
                {
                    { "iss_retido", "" },
                    { "responsavel_retencao", "" },
                    { "valor_base_calculo_iss", "" },
                    { "aliquota_iss", "" },
                    { "valor_iss", "" }
                }
            },

            { "tributos_nacionais", new Dictionary<string, object>
                {
                    { "valor_pis", "" },
                    { "valor_cofins", "" },
                    { "valor_inss", "" },
                    { "valor_ir", "" },
                    { "valor_csll", "" },
                    { "valor_outras", "" }
                }
            }
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
