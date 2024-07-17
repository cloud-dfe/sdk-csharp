using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOjQ2MSwidXNyIjoxNzAsInRwIjoyLCJpYXQiOjE2NTE1MDYzMjR9.a0cOwP6BUDZAboYwMzoMjutCtFM8Ph-X4pLahZIB_V4" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var nfse = new Nfse(config);

var payload = new Dictionary<string, object>
{
    { "data_emissao_inicial", "2020-01-01" },
    { "data_emissao_final", "2020-01-31" },
    { "data_competencia_inicial", "2020-01-01" },
    { "data_competencia_final", "2020-01-31" },
    { "tomador_cnpj", null },
    { "tomador_cpf", null },
    { "tomador_im", null },
    { "nfse_numero", null },
    { "nfse_numero_inicial", null },
    { "nfse_numero_final", null },
    { "rps_numero", "15" },
    { "rps_serie", "0" },
    { "rps_tipo", "1" },
    { "pagina", "1" }
};

try
{
    var resp = Task.Run(async () => await nfse.Localiza(payload)).GetAwaiter().GetResult();

    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
