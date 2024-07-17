using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOjQ2MSwidXNyIjoxNzAsInRwIjoyLCJpYXQiOjE2NTE1MDYzMjR9.a0cOwP6BUDZAboYwMzoMjutCtFM8Ph-X4pLahZIB_V4" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var cteos = new Cteos(config);

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

    var resp = Task.Run(async () => await cteos.Preview(payload)).GetAwaiter().GetResult();
    
    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
