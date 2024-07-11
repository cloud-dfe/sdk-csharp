using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOjQ2MSwidXNyIjoxNzAsInRwIjoyLCJpYXQiOjE2NTE1MDYzMjR9.a0cOwP6BUDZAboYwMzoMjutCtFM8Ph-X4pLahZIB_V4" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", false }
};

var averbacao = new Averbacao(config);

try
{

    string xmlFile = Util.ReadFile("caminho_do_arquivo.xml");

    string xmlFileBase64 = Util.Encode(xmlFile);

    var payload = new Dictionary<string, object>
    {
        {"xml", xmlFileBase64},
        {"usuario", "login"},
        {"senha", "senha"},
        {"chave", "50000000000000000000000000000000000000000000"}
    };

    var resp = Task.Run(async () => await averbacao.PortoSeguro(payload)).GetAwaiter().GetResult();
    
    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}