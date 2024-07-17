using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOjQ2MSwidXNyIjoxNzAsInRwIjoyLCJpYXQiOjE2NTE1MDYzMjR9.a0cOwP6BUDZAboYwMzoMjutCtFM8Ph-X4pLahZIB_V4" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var nfe = new Nfe(config);

var payload = new Dictionary<string, object>
{
    {"chave", "123456789012345678901234567890123456789012345678901234"},
    {"registra", new Dictionary<string, object>
        {
            {"data", "2021-10-12T12:22:33-03:00"},
            {"imagem", "lUHJvYyB2ZXJzYW...."},
            {"recebedor_documento", "123456789 SSPRJ"},
            {"recebedor_nome", "NOME TESTE"},
            {"coordenadas", new Dictionary<string, object>
                {
                    {"latitude", -23.628360},
                    {"longitude", -46.622109}
                }
            }
        }
    }
};

try
{
    var resp = Task.Run(async () => await nfe.Comprovante(payload)).GetAwaiter().GetResult();

    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
