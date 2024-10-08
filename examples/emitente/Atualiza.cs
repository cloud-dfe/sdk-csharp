using Newtonsoft.Json;
using Sdk.CloudDfe;

var config = new Dictionary<string, object>
{
    { "token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXAiOiJ0b2tlbl9leGVtcGxvIiwidXNyIjoidGsiLCJ0cCI6InRrIn0.Tva_viCMCeG3nkRYmi_RcJ6BtSzui60kdzIsuq5X-sQ" },
    { "ambiente", Consts.AMBIENTE_HOMOLOGACAO },
    { "timeout", 60 },
    { "debug", true }
};

var emitente = new Emitente(config);

try
{

    var payload = new Dictionary<string, object>
    {
        {"nome", "EMPRESA TESTE 2"},
        {"razao", "EMPRESA TESTE 2"},
        // {"cnae", "12369875"},
        // {"crt", "1"}, // Regime tributário
        // {"ie", "12369875"},
        // {"im", "12369875"},
        // {"suframa", "12369875"},
        // {"csc", "...",} // token para emissão de NFCe
        // {"cscid", "000001"},
        // {"tar", "C92920029-12",} // tar BPe
        // {"login_prefeitura", null},
        // {"senha_prefeitura", null},
        // {"client_id_prefeitura", null},
        // {"client_secret_prefeitura", null},
        // {"telefone", "46998895532"},
        // {"email", "empresa@teste.com",}
        // {"rua", "TESTE"},
        // {"numero", "1"},
        // {"complemento", "NENHUM"},
        // {"bairro", "TESTE"},
        // {"municipio", "CIDADE TESTE",} // IBGE
        // {"cmun", "5300108"}, // IBGE
        // {"uf", "PR"}, // IBGE
        // {"cep", "85000100"},
        // {"logo": "useyn56j4mx35m5j6_JSHh734khjd...saasjda"},// BASE 64
    };

    var resp = Task.Run(async () => await emitente.Atualiza(payload)).GetAwaiter().GetResult();
    
    string jsonOutput = JsonConvert.SerializeObject(resp, Formatting.Indented);
    Console.WriteLine(jsonOutput);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao obter o status: {ex.Message}");
}
