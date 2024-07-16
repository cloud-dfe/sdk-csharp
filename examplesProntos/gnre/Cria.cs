using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOjQ2MSwidXNyIjoxNzAsInRwIjoyLCJpYXQiOjE2NTE1MDYzMjR9.a0cOwP6BUDZAboYwMzoMjutCtFM8Ph-X4pLahZIB_V4" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var gnre = new Gnre(config);

var payload = new Dictionary<string, object>
{
    {"numero", "6"},
    {"uf_favoverida", "RO"},
    {"ie_emitente_uf_favorecida", null},
    {"tipo", "0"},
    {"valor", 12.55},
    {"data_pagamento", "2022-05-22"},
    {"identificador_guia", "12345"},
    {"receitas", new List<Dictionary<string, object>>
        {
            new Dictionary<string, object>
            {
                {"codigo", "100102"},
                {"detalhamento", null},
                {"data_vencimento", "2022-05-22"},
                {"convenio", "Convênio ICMS 142/18"},
                {"numero_controle", "1"},
                {"numero_controle_fecp", null},
                {"documento_origem", new Dictionary<string, object>
                    {
                        {"numero", "000000001"},
                        {"tipo", "10"}
                    }
                },
                {"produto", null},
                {"referencia", new Dictionary<string, object>
                    {
                        {"periodo", "0"},
                        {"mes", "05"},
                        {"ano", "2022"},
                        {"parcela", null}
                    }
                },
                {"valores", new List<Dictionary<string, object>>
                    {
                        new Dictionary<string, object>
                        {
                            {"valor", 12.55},
                            {"tipo", "11"}
                        }
                    }
                },
                {"contribuinte_destinatario", new Dictionary<string, object> 
                    {
                        {"cnpj", null},
                        {"cpf", null},
                        {"ie", null},
                        {"razao", null},
                        {"ibge", null}
                    }
                },
                {"extras", new List<Dictionary<string, object>>
                    {
                        new Dictionary<string, object>
                        {
                            {"codigo", "52"},
                            {"conteudo", "32220526434850000191550100000000011015892724"}
                        }        
                    }
                }
            } 
        }
    }
};

try
{

    var resp = Task.Run(async () => await gnre.Cria(payload)).GetAwaiter().GetResult();
    
    if (resp.ContainsKey("sucesso") && (bool)resp["sucesso"])
        {
            var chave = resp["chave"].ToString();
            Thread.Sleep(5000);
            int tentativa = 1;

            while (tentativa <= 5)
            {
                var payloadConsulta = new Dictionary<string, object> { { "chave", chave } };
                var respC = Task.Run(async () => await gnre.Consulta(payloadConsulta)).GetAwaiter().GetResult();

                if (!respC.ContainsKey("codigo") || Convert.ToInt32(respC["codigo"]) != 5023)
                {
                    if (respC.ContainsKey("sucesso") && (bool)respC["sucesso"])
                    {
                        string jsonOutput = JsonConvert.SerializeObject(respC, Formatting.Indented);
                        Console.WriteLine(jsonOutput);
                        break;
                    }
                }
                else
                {
                    string jsonOutput = JsonConvert.SerializeObject(respC, Formatting.Indented);
                    Console.WriteLine(jsonOutput);
                    break;
                }

                Thread.Sleep(5000);
                tentativa += 1;
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
        else if (resp.ContainsKey("codigo") && (Convert.ToInt32(resp["codigo"]) == 5008 || Convert.ToInt32(resp["codigo"]) >= 7000))
        {
            var chave = resp["chave"].ToString();
            string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
            Console.WriteLine(jsonOutput);

            var payloadConsulta = new Dictionary<string, object> { { "chave", chave } };
            var respC = Task.Run(async () => await gnre.Consulta(payloadConsulta)).GetAwaiter().GetResult();

            if (respC.ContainsKey("sucesso") && (bool)respC["sucesso"])
            {
                if (respC.ContainsKey("codigo") && Convert.ToInt32(respC["codigo"]) == 5023)
                {
                    jsonOutput = JsonConvert.SerializeObject(respC, Formatting.Indented);
                    Console.WriteLine(jsonOutput);
                }
            }
            else
            {
                jsonOutput = JsonConvert.SerializeObject(respC, Formatting.Indented);
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
