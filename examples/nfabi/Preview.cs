using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOiJ0b2tlbl9leGVtcGxvIiwidXNyIjoidGsiLCJ0cCI6InRrIn0.Tva_viCMCeG3nkRYmi_RcJ6BtSzui60kdzIsuq5X-sQ" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var nfabi = new Nfabi(config);

var payload = JsonConvert.DeserializeObject<Dictionary<string, object>>(@"{
    ""identificacao"": ""teste-nfabi-1"",
    ""numero"": ""123"",
    ""serie"": ""1"",
    ""data_emissao"": ""2026-12-14T10:00:00-03:00"",
    ""tipo_documento"": ""0"",
    ""finalidade"": ""1"",
    ""tipo_emitente"": ""01"",
    ""operacao"": {
        ""modalidade"": ""01"",
        ""natureza"": ""01"",
        ""detalhamento"": ""02"",
        ""valor_total"": ""500000.00"",
        ""incorporacao"": {
            ""indicador"": ""0""
        },
        ""instrumento"": {
            ""tipo"": ""03"",
            ""data"": ""2026-11-10""
        }
    },
    ""transmitentes"": [
        {
            ""cnpj"": ""10944071000104"",
            ""nome"": ""VENDEDOR TESTE LTDA"",
            ""contribuinte"": ""1""
        }
    ],
    ""adquirentes"": [
        {
            ""cpf"": ""98765432100"",
            ""nome"": ""Comprador Teste"",
            ""endereco"": {
                ""logradouro"": ""Rua A"",
                ""numero"": ""10"",
                ""bairro"": ""Centro"",
                ""codigo_municipio"": ""2927408"",
                ""nome_municipio"": ""Salvador"",
                ""uf"": ""BA"",
                ""cep"": ""40000000""
            }
        }
    ],
    ""imovel"": {
        ""tipo"": ""1"",
        ""especie"": ""02"",
        ""enquadramento"": ""02"",
        ""area_total"": ""250"",
        ""unidade_area"": ""m2"",
        ""matricula"": ""9999"",
        ""cadastro"": {
            ""possui_iptu"": true,
            ""iptu"": ""123456""
        },
        ""endereco"": {
            ""logradouro"": ""Rua B"",
            ""numero"": ""20"",
            ""bairro"": ""Centro"",
            ""codigo_municipio"": ""2927408"",
            ""nome_municipio"": ""Salvador"",
            ""uf"": ""BA"",
            ""cep"": ""40000000""
        }
    },
    ""tributacao"": {
        ""cst"": ""200"",
        ""classificacao_tributaria"": ""200046"",
        ""ibscbs"": {
            ""valor_operacao"": ""500000.00"",
            ""base_calculo"": ""500000.00"",
            ""ibs_uf"": {
                ""aliquota"": ""0.1000"",
                ""reducao_aliquota"": ""50.0000"",
                ""aliquota_efetiva"": ""0.0500"",
                ""valor"": ""250.00""
            },
            ""ibs_municipal"": {
                ""aliquota"": ""0.0000"",
                ""reducao_aliquota"": ""50.0000"",
                ""aliquota_efetiva"": ""0.0000"",
                ""valor"": ""0.00""
            },
            ""cbs"": {
                ""aliquota"": ""0.9000"",
                ""reducao_aliquota"": ""50.0000"",
                ""aliquota_efetiva"": ""0.4500"",
                ""valor"": ""2250.00""
            }
        }
    },
    ""pagamentos"": {
        ""imovel"": [
            {
                ""tipo"": ""01"",
                ""valor"": ""500000.00""
            }
        ]
    },
    ""informacoes_adicionais_contribuinte"": ""Teste local""
}")!;

try
{
    var resp = Task.Run(async () => await nfabi.Preview(payload)).GetAwaiter().GetResult();

    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
