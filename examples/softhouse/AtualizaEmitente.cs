using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOjQ2MSwidXNyIjoxNzAsInRwIjoyLCJpYXQiOjE2NTE1MDYzMjR9.a0cOwP6BUDZAboYwMzoMjutCtFM8Ph-X4pLahZIB_V4" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var soft = new Softhouse(config);

var payload = new Dictionary<string, object>
{
    { "nome", "EMPRESA TESTE" },
    { "razao", "EMPRESA TESTE" },
    { "cnae", "12369875" },
    { "crt", "1" },
    { "ie", "12369875" },
    { "im", "12369875" },
    { "suframa", "12369875" },
    { "csc", "..." },
    { "cscid", "000001" },
    { "tar", "C92920029-12" },
    { "login_prefeitura", null },
    { "senha_prefeitura", null },
    { "client_id_prefeitura", null },
    { "client_secret_prefeitura", null },
    { "telefone", "46998895532" },
    { "email", "empresa@teste.com" },
    { "rua", "TESTE" },
    { "numero", "1" },
    { "complemento", "NENHUM" },
    { "bairro", "TESTE" },
    { "municipio", "CIDADE TESTE" },
    { "cmun", "5300108" },
    { "uf", "PR" },
    { "cep", "85000100" },
    { "logo", "useyn56j4mx35m5j6_JSHh734khjd...saasjda" },
    { "webhook", "https://seusite.com.br/notifications" }
};

try
{
    var resp = Task.Run(async () => await soft.AtualizaEmitente(payload)).GetAwaiter().GetResult();
    
    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
