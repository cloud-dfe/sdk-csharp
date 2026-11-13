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
    ""chave"": ""41261210944071000104770010000001231012345679"",
    ""codigo"": ""01"",
    ""valor_parcela"": ""10000.00"",
    ""valor_total_parcela"": ""10000.00"",
    ""data_pagamento"": ""2027-01-10"",
    ""tributacao"": {
        ""cst"": ""200"",
        ""classificacao_tributaria"": ""200046"",
        ""valor_operacao"": ""10000.00"",
        ""base_calculo"": ""10000.00"",
        ""ibs_uf"": {
            ""aliquota"": ""0.1000"",
            ""reducao_aliquota"": ""50.0000"",
            ""aliquota_efetiva"": ""0.0500"",
            ""valor"": ""5.00""
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
            ""valor"": ""45.00""
        }
    }
}")!;

try
{
    var resp = Task.Run(async () => await nfabi.Parcela(payload)).GetAwaiter().GetResult();

    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
