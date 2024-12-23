using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOiJ0b2tlbl9leGVtcGxvIiwidXNyIjoidGsiLCJ0cCI6InRrIn0.Tva_viCMCeG3nkRYmi_RcJ6BtSzui60kdzIsuq5X-sQ" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var mdfe = new Mdfe(config);

var payload = new Dictionary<string, object>
{
    {"tipo_operacao", "2"},
    {"tipo_transporte", null},
    {"numero", "27"},
    {"serie", "1"},
    {"data_emissao", "2021-06-26T09:21:42-00:00"},
    {"uf_inicio", "RN"},
    {"uf_fim", "GO"},
    {"municipios_carregamento", new List<Dictionary<string, object>>
        {
            new Dictionary<string, object>
            {
                {"codigo_municipio", "2408003"},
                {"nome_municipio", "Mossoró"}
            }
        }
    },
    {"percursos", new List<Dictionary<string, object>>
        {
            new Dictionary<string, object> { {"uf", "PB"} },
            new Dictionary<string, object> { {"uf", "PE"} },
            new Dictionary<string, object> { {"uf", "BA"} }
        }
    },
    {"municipios_descarregamento", new List<Dictionary<string, object>>
        {
            new Dictionary<string, object>
            {
                {"codigo_municipio", "5200050"},
                {"nome_municipio", "Abadia de Goiás"},
                {"nfes", new List<Dictionary<string, object>>
                    {
                        new Dictionary<string, object>
                        {
                            {"chave", "50000000000000000000000000000000000000000000"}
                        }
                    }
                }
            }
        }
    },
    {"valores", new Dictionary<string, object>
        {
            {"valor_total_carga", "100.00"},
            {"codigo_unidade_medida_peso_bruto", "01"},
            {"peso_bruto", "1000.000"}
        }
    },
    {"informacao_adicional_fisco", null},
    {"informacao_complementar", null},
    {"modal_rodoviario", new Dictionary<string, object>
        {
            {"rntrc", "57838055"},
            {"ciot", new List<object>()},
            {"contratante", new List<object>()},
            {"vale_pedagio", new List<object>()},
            {"veiculo", new Dictionary<string, object>
                {
                    {"codigo", "000000001"},
                    {"placa", "FFF1257"},
                    {"renavam", "335540391"},
                    {"tara", "0"},
                    {"tipo_rodado", "01"},
                    {"tipo_carroceria", "00"},
                    {"uf", "MT"},
                    {"condutores", new List<Dictionary<string, object>>
                        {
                            new Dictionary<string, object>
                            {
                                {"nome", "JOAO TESTE"},
                                {"cpf", "01234567890"}
                            }
                        }
                    }
                }
            },
            {"reboques", new List<object>()}
        }
    }
};

try
{
    var resp = Task.Run(async () => await mdfe.Cria(payload)).GetAwaiter().GetResult();

    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

    if (resp.ContainsKey("sucesso") && (bool)resp["sucesso"]){
        if (resp.ContainsKey("codigo") && Convert.ToInt32(resp["codigo"]) == 2){
            // Offline
            Console.WriteLine("Documento offline. Aguarde a notificação.");
        }
        else{
            // Autorizado
            Console.WriteLine($"Documento autorizado: {JsonConvert.SerializeObject(resp, Formatting.Indented)}");
        }
    }
    else if (resp.ContainsKey("codigo") && (Convert.ToInt32(resp["codigo"]) == 5001 || Convert.ToInt32(resp["codigo"]) == 5002)){
        // Erro nos campos
        if (resp.ContainsKey("erros")){
            Console.WriteLine($"Erro nos campos: {JsonConvert.SerializeObject(resp["erros"], Formatting.Indented)}");
        }
        else{
            Console.WriteLine("Erro nos campos, mas sem detalhes disponíveis.");
        }
    }
    else if (resp.ContainsKey("codigo") && (Convert.ToInt32(resp["codigo"]) == 5008 || Convert.ToInt32(resp["codigo"]) >= 7000)){
        string chave = resp.ContainsKey("chave") ? resp["chave"]?.ToString() : null;

        if (string.IsNullOrEmpty(chave)){
            Console.WriteLine("Chave não encontrada no response.");
            return;
        }

        // >= 7000 indica problemas de comunicação com a SEFAZ
        Console.WriteLine($"Problemas de comunicação ou chave pendente: {JsonConvert.SerializeObject(resp, Formatting.Indented)}");

        var payloadConsulta = new Dictionary<string, object> { { "chave", chave } };

        try{
            var respConsulta = Task.Run(async () => await mdfe.Consulta(payloadConsulta)).GetAwaiter().GetResult();

            if (respConsulta.ContainsKey("codigo") && Convert.ToInt32(respConsulta["codigo"]) != 5023){
                if (respConsulta.ContainsKey("sucesso") && (bool)respConsulta["sucesso"]){
                    // Autorizado
                    Console.WriteLine($"Documento autorizado após consulta: {JsonConvert.SerializeObject(respConsulta, Formatting.Indented)}");
                }
                else{
                    // Rejeição
                    Console.WriteLine($"Documento rejeitado após consulta: {JsonConvert.SerializeObject(respConsulta, Formatting.Indented)}");
                }
            }
            else{
                // Em processamento
                Console.WriteLine($"Documento em processamento: {JsonConvert.SerializeObject(respConsulta, Formatting.Indented)}");
            }
        }
        catch (Exception ex){
            Console.WriteLine($"Erro ao consultar documento: {ex.Message}");
        }
    }
    else{
        // Rejeição
        Console.WriteLine($"Documento rejeitado: {JsonConvert.SerializeObject(resp, Formatting.Indented)}");
    }
}
catch (ArgumentException ex){
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
