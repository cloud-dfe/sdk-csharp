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
        {"chave", "50000000000000000000000000000000000000000000"},
        {"correcoes", new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    {"grupo_corrigido", "grupoCorrigidoValue"},
                    {"campo_corrigido", "campoCorrigidoValue"},
                    {"valor_corrigido", "valorCorrigidoValue"}
                }
            }
        }
    };

    var resp = Task.Run(async () => await cte.Correcao(payload)).GetAwaiter().GetResult();
    
    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
