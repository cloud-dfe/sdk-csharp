using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOiJ0b2tlbl9leGVtcGxvIiwidXNyIjoidGsiLCJ0cCI6InRrIn0.Tva_viCMCeG3nkRYmi_RcJ6BtSzui60kdzIsuq5X-sQ" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var cte = new Cte(config);

try
{

    var payload = new Dictionary<string, object>
    {
        {"cfop", "5932"},
        {"natureza_operacao", "PRESTACAO DE SERVIÇO"},
        {"numero", "66"},
        {"serie", "1"},
        {"data_emissao", "2021-06-22T03:00:00-03:00"},
        {"tipo_operacao", "0"},
        {"codigo_municipio_envio", "2408003"},
        {"nome_municipio_envio", "MOSSORO"},
        {"uf_envio", "RN"},
        {"tipo_servico", "0"},
        {"codigo_municipio_inicio", "2408003"},
        {"nome_municipio_inicio", "Mossoró"},
        {"uf_inicio", "RN"},
        {"codigo_municipio_fim", "2408003"},
        {"nome_municipio_fim", "Mossoró"},
        {"uf_fim", "RN"},
        {"retirar_mercadoria", "1"},
        {"detalhes_retirar", null},
        {"tipo_programacao_entrega", "0"},
        {"sem_hora_tipo_hora_programada", "0"},
        {"remetente", new Dictionary<string, object>
            {
                {"cpf", "01234567890"},
                {"inscricao_estadual", null},
                {"nome", "EMPRESA MODELO"},
                {"razao_social", "MODELO LTDA"},
                {"telefone", "8433163070"},
                {"endereco", new Dictionary<string, object>
                    {
                        {"logradouro", "AVENIDA TESTE"},
                        {"numero", "444"},
                        {"bairro", "CENTRO"},
                        {"codigo_municipio", "2408003"},
                        {"nome_municipio", "MOSSORÓ"},
                        {"uf", "RN"}
                    }
                }
            }
        },
        {"valores", new Dictionary<string, object>
            {
                {"valor_total", "0.00"},
                {"valor_receber", "0.00"},
                {"valor_total_carga", "224.50"},
                {"produto_predominante", "SAL"},
                {"quantidades", new List<Dictionary<string, object>>
                    {
                        new Dictionary<string, object>
                        {
                            {"codigo_unidade_medida", "01"},
                            {"tipo_medida", "Peso Bruto"},
                            {"quantidade", "500.00"}
                        }
                    }
                }
            }
        },
        {"imposto", new Dictionary<string, object>
            {
                {"icms", new Dictionary<string, object>
                    {
                        {"situacao_tributaria", "20"},
                        {"valor_base_calculo", "0.00"},
                        {"aliquota", "12.00"},
                        {"valor", "0.00"},
                        {"aliquota_reducao_base_calculo", "50.00"}
                    }
                },
            }
        },
        {"nfes", new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    {"chave", "50000000000000000000000000000000000000000000"}
                },
            }
        },
        {"modal_rodoviario", new Dictionary<string, object>
            {
                {"rntrc", "02033517"}
            }
        },
        {"destinatario", new Dictionary<string, object>
            {
                {"cpf", "01234567890"},
                {"inscricao_estadual", null},
                {"nome", "EMPRESA MODELO"},
                {"telefone", "8499995555"},
                {"endereco", new Dictionary<string, object>
                    {
                        {"logradouro", "AVENIDA TESTE"},
                        {"numero", "444"},
                        {"bairro", "CENTRO"},
                        {"codigo_municipio", "2408003"},
                        {"nome_municipio", "Mossoró"},
                        {"cep", "59603330"},
                        {"uf", "RN"},
                        {"codigo_pais", "1058"},
                        {"nome_pais", "BRASIL"},
                        {"email", "teste@teste.com.br"}
                    }
                }
            }
        },
        {"componentes_valor", new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                    {
                        {"nome", "teste2"},
                        {"valor", "1999.00"}
                    }
            }
        },
        {"tomador", new Dictionary<string, object>
            {
                {"tipo", "3"},
                {"indicador_inscricao_estadual", "9"}
            }
        },
        {"observacao", ""}
    };

    var resp = Task.Run(async () => await cte.Cria(payload)).GetAwaiter().GetResult();

    if (resp.ContainsKey("sucesso") && (bool)resp["sucesso"])
    {
        var chave = resp["chave"].ToString();
        Thread.Sleep(5000);
        int tentativa = 1;

        while (tentativa <= 5)
        {
            var payloadConsulta = new Dictionary<string, object> { { "chave", chave } };
            var respC = Task.Run(async () => await cte.Consulta(payloadConsulta)).GetAwaiter().GetResult();

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
    else if (resp.ContainsKey("codigo") && (Convert.ToInt32(resp["codigo"]) == 5008)
        {
        var chave = resp["chave"].ToString();
        string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
        Console.WriteLine(jsonOutput);

        var payloadConsulta = new Dictionary<string, object> { { "chave", chave } };
        var respC = Task.Run(async () => await cte.Consulta(payloadConsulta)).GetAwaiter().GetResult();

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
