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
            {"reboque", new List<object>()}
        }
    }
};

try
{
    var resp = Task.Run(async () => await mdfe.Preview(payload)).GetAwaiter().GetResult();

    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
