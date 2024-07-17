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

var payload = new Dictionary<string, object>
{
    {"numero_inicial", "1214"},
    {"numero_final", "101002"},
    {"serie", "1"},
    // {"data_inicial", "2019-12-01"},
    // {"data_final", "2019-12-31"},
    // {"cancel_inicial", "2019-12-01"} // - Cancelamento
    // {"cancel_final", "2019-12-31"}
};

try
{
    var resp = Task.Run(async () => await nfce.Busca(payload)).GetAwaiter().GetResult();

    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
