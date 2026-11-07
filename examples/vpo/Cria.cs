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
    {"numero", "1001"},
    {"identificacao", "VIAGEM-77"},
    {"emissor", "SEMPARAR"},
    {"data_inicio", "2026-11-10 08:00:00"},
    {"data_fim", "2026-11-12 18:00:00"},
    {"rota", new Dictionary<string, object>
    {
        {"id", "58222"},
        {"indice", "0"},
        {"distancia_km", "45.279"},
        {"tipo_combustivel", "DIESEL"}
    }},
    {"veiculo", new Dictionary<string, object>
    {
        {"placa", "BCD2E34"},
        {"categoria", "7"}
    }},
    {"transportador", new Dictionary<string, object>
    {
        {"cnpj", "22333444000181"},
        {"rntrc", "87654321"},
        {"nome", "TRANSPORTADOR EXEMPLO LTDA"}
    }},
    {"cte", "1234"}
};

try
{
    var resp = Task.Run(async () => await vpo.Cria(payload)).GetAwaiter().GetResult();

    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
