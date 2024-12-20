using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOiJ0b2tlbl9leGVtcGxvIiwidXNyIjoidGsiLCJ0cCI6InRrIn0.Tva_viCMCeG3nkRYmi_RcJ6BtSzui60kdzIsuq5X-sQ" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var soft = new Softhouse(config);

var payload = new Dictionary<string, object>
{
    { "nome", "EMPRESA TESTE" },
    { "razao", "EMPRESA TESTE" },
    { "cnpj", "47853098000193" },
    { "cpf", "12345678901" },
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
    { "plano", "Emitente" },
    { "documentos", new Dictionary<string, object>
        {
            { "nfe", true },
            { "nfce", true },
            { "nfse", true },
            { "mdfe", true },
            { "cte", true },
            { "cteos", true },
            { "bpe", true },
            { "dfe_nfe", true },
            { "dfe_cte", true },
            { "gnre", true }
        }
    }
};


try
{
    var resp = Task.Run(async () => await soft.CriaEmitente(payload)).GetAwaiter().GetResult();

    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
