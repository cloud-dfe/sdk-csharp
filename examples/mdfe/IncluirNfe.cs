using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOjQ2MSwidXNyIjoxNzAsInRwIjoyLCJpYXQiOjE2NTE1MDYzMjR9.a0cOwP6BUDZAboYwMzoMjutCtFM8Ph-X4pLahZIB_V4" },
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
