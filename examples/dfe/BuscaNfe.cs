using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOiJ0b2tlbl9leGVtcGxvIiwidXNyIjoidGsiLCJ0cCI6InRrIn0.Tva_viCMCeG3nkRYmi_RcJ6BtSzui60kdzIsuq5X-sQ" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var dfe = new Dfe(config);

try
{

    var payload = new Dictionary<string, object>
    {
        {"periodo", "2020-10"},
        {"data", "2020-10-15"},
        {"cnpj", "06338788000127"}
    };

    var resp = Task.Run(async () => await dfe.BuscaNfe(payload)).GetAwaiter().GetResult();
    
    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

    if (resp.ContainsKey("sucesso") && Convert.ToBoolean(resp["sucesso"]))
    {
        if (resp["docs"] is JArray docs)
        {
            foreach (var doc in docs)
            {
                string chave = (string)doc["chave"];
                Console.WriteLine($"Doc Chave: {chave}");
            }
        }

        if (resp["eventos_proprios"] is JArray eventosProprios)
        {
            foreach (var evento in eventosProprios)
            {
                string chave = (string)evento["chave"];
                Console.WriteLine($"Evento Chave: {chave}");
            }
        }
    }
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
