using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOiJ0b2tlbl9leGVtcGxvIiwidXNyIjoidGsiLCJ0cCI6InRrIn0.Tva_viCMCeG3nkRYmi_RcJ6BtSzui60kdzIsuq5X-sQ" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var vpo = new Vpo(config);

var payload = new Dictionary<string, object>
{
    {"enderecos", new List<object>
    {
        new Dictionary<string, object>
        {
            {"codigo_municipio", "3543402"},
            {"nome_municipio", "Ribeirão Preto"},
            {"uf", "SP"}
        },
        new Dictionary<string, object>
        {
            {"codigo_municipio", "3505906"},
            {"nome_municipio", "Batatais"},
            {"uf", "SP"}
        }
    }},
    {"categoria", "7"},
    {"data_inicio", "2026-11-10 08:00:00"},
    {"tipo_combustivel", "DIESEL"}
};

try
{
    var resp = Task.Run(async () => await vpo.Rota(payload)).GetAwaiter().GetResult();

    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
