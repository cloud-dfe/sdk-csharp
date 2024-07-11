using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOjQ2MSwidXNyIjoxNzAsInRwIjoyLCJpYXQiOjE2NTE1MDYzMjR9.a0cOwP6BUDZAboYwMzoMjutCtFM8Ph-X4pLahZIB_V4" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var cteos = new Cteos(config);

try
{

    var payload = new Dictionary<string, object>
    {
        {"numero_inicial", "1214"},
        {"numero_final", "101002"},
        {"serie", "1"},
        //{"data_inicial", "2019-12-01"},
        //{"data_final", "2019-12-31"},
        //{"cancel_inicial", "2019-12-01"},
        //{"cancel_final", "2019-12-31"},
    };

    var resp = Task.Run(async () => await cteos.Busca(payload)).GetAwaiter().GetResult();
    
    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
