using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOiJ0b2tlbl9leGVtcGxvIiwidXNyIjoidGsiLCJ0cCI6InRrIn0.Tva_viCMCeG3nkRYmi_RcJ6BtSzui60kdzIsuq5X-sQ" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var nfce = new Nfce(config);

var listaItens = new List<Dictionary<string, object>>
{
    new Dictionary<string, object>
    {
        {"numero_item", "1"},
        {"codigo_produto", "000297"},
        {"descricao", "SAL GROSSO 50KGS"},
        {"codigo_ncm", "84159020"},
        {"cfop", "5102"},
        {"unidade_comercial", "SC"},
        {"quantidade_comercial", 10},
        {"valor_unitario_comercial", "22.45"},
        {"valor_bruto", "224.50"},
        {"unidade_tributavel", "SC"},
        {"quantidade_tributavel", "10.00"},
        {"valor_unitario_tributavel", "22.45"},
        {"origem", "0"},
        {"inclui_no_total", "1"},
        {"imposto", new Dictionary<string, object>
            {
                {"valor_aproximado_tributos", 9.43},
                {"icms", new Dictionary<string, object>
                    {
                        {"situacao_tributaria", "102"},
                        {"aliquota_credito_simples", "0"},
                        {"valor_credito_simples", "0"},
                        {"modalidade_base_calculo", "3"},
                        {"valor_base_calculo", "0.00"},
                        {"modalidade_base_calculo_st", "4"},
                        {"aliquota_reducao_base_calculo", "0.00"},
                        {"aliquota", "0.00"},
                        {"aliquota_final", "0.00"},
                        {"valor", "0.00"},
                        {"aliquota_margem_valor_adicionado_st", "0.00"},
                        {"aliquota_reducao_base_calculo_st", "0.00"},
                        {"valor_base_calculo_st", "0.00"},
                        {"aliquota_st", "0.00"},
                        {"valor_st", "0.00"}
                    }
                },
                {"fcp", new Dictionary<string, object>
                    {
                        {"aliquota", "1.65"}
                    }
                },
                {"pis", new Dictionary<string, object>
                    {
                        {"situacao_tributaria", "01"},
                        {"valor_base_calculo", 224.5},
                        {"aliquota", "1.65"},
                        {"valor", "3.70"}
                    }
                },
                {"cofins", new Dictionary<string, object>
                    {
                        {"situacao_tributaria", "01"},
                        {"valor_base_calculo", 224.5},
                        {"aliquota", "7.60"},
                        {"valor", "17.06"}
                    }
                }
            }
        },
        {"valor_desconto", 0},
        {"valor_frete", 0},
        {"valor_seguro", 0},
        {"valor_outras_despesas", 0},
        {"informacoes_adicionais_item", "Valor aproximado tributos R$: 9,43 (4,20%) Fonte: IBPT"}
    }
};

var payload = new Dictionary<string, object>
{
    {"natureza_operacao", "VENDA DENTRO DO ESTADO"},
    {"serie", "1"},
    {"numero", "101008"},
    {"data_emissao", "2021-06-26T15:20:00-03:00"},
    {"tipo_operacao", "1"},
    {"presenca_comprador", "1"},
    {"itens", listaItens},
    {"frete", new Dictionary<string, object>
        {
            {"modalidade_frete", "9"}
        }
    },
    {"pagamento", new Dictionary<string, object>
        {
            {"formas_pagamento", new List<Dictionary<string, object>>
                {
                    new Dictionary<string, object>
                    {
                        {"meio_pagamento", "01"},
                        {"valor", "224.50"}
                    }
                }
            }
        }
    },
    {"informacoes_adicionais_contribuinte", "PV: 3325 * Rep: DIRETO * Motorista:  * Forma Pagto: 04 DIAS * teste de observação para a nota fiscal * Valor aproximado tributos R$9,43 (4,20%) Fonte: IBPT"},
    {"pessoas_autorizadas", new List<Dictionary<string, object>>
        {
            new Dictionary<string, object>
            {
                {"cnpj", "96256273000170"}
            },
            new Dictionary<string, object>
            {
                {"cnpj", "80681257000195"}
            }
        }
    }
};

try
{
    var resp = Task.Run(async () => await nfce.Cria(payload)).GetAwaiter().GetResult();

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
            var respConsulta = Task.Run(async () => await nfce.Consulta(payloadConsulta)).GetAwaiter().GetResult();

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
