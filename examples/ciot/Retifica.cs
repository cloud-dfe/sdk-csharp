using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOiJ0b2tlbl9leGVtcGxvIiwidXNyIjoidGsiLCJ0cCI6InRrIn0.Tva_viCMCeG3nkRYmi_RcJ6BtSzui60kdzIsuq5X-sQ" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var ciot = new Ciot(config);

var payload = new Dictionary<string, object>
{
    {"identificacao", "VIAGEM-0001"},
    {"data_fim", "2026-11-13"},
    {"valor_frete", "4800.00"},
    {"pagamentos", new List<object>
    {
        new Dictionary<string, object>
        {
            {"forma", "1"},
            {"tipo", "5"},
            {"cnpj", "22333444000181"},
            {"chave_pix", "22333444000181"},
            {"vencimento", "2026-11-13"},
            {"valor", "4800.00"}
        }
    }}
};

try
{
    var resp = Task.Run(async () => await ciot.Retifica(payload)).GetAwaiter().GetResult();

    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
