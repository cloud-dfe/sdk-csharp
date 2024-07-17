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
    {"chave", "50000000000000000000000000000000000000000000"},
    {"codigo_municipio_carregamento", "2408003"},
    {"nome_municipio_carregamento", "Mossoró"},
    {"codigo_municipio_descarregamento", "5200050"},
    {"nome_municipio_descarregamento", "Abadia de Goiás"},
    {"chave_nfe", "50000000000000000000000000000000000000000001"}
};

try
{
    var resp = Task.Run(async () => await mdfe.Nfe(payload)).GetAwaiter().GetResult();

    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
